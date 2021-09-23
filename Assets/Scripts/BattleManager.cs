using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BattleManager : StateMachine
{
	public List<Enemy> enemies = new List<Enemy>();
	public List<Card> cards = new List<Card>();
	public Enemy currentEnemy;

	public bool playerMove;

	public void Awake()
	{
		SetState(new MenuState());
	}

	public void NextEnemy()
	{
		if (enemies.Where(x => x.health > 0).Count() <= 0)
        {
			Debug.Log("You win!");
			GameManager.Instance.win.gameObject.SetActive(true);
			GameManager.Instance.button.SetActive(true);
		}

		if (currentEnemy != null)
		{
			enemies.Remove(currentEnemy);
			Destroy(currentEnemy.gameObject);
		}
		currentEnemy = enemies.Where(x => x != null).OrderBy(x => x.health).FirstOrDefault();
		currentEnemy.transform.SetParent(GameManager.Instance.enemyHolder.transform);
		currentEnemy.transform.localPosition = Vector3.zero;

		Debug.Log("enemy name: " + currentEnemy.name + "   health: " + currentEnemy.health + "  attack damage 1: " + currentEnemy.attackDamage[0] + " stagger: " + currentEnemy.staggerTotal);
	}

	public void ResetGame()
	{
		SetState(new MenuState());
		cards.Clear();
		enemies.Clear();
	}

	public void PlayCard(Card playedCard)
    {
		if (currentEnemy == null)
		{
			NextEnemy();
		}

		// Put the card on the board
		playedCard.SetState(new ArenaState(playedCard));

		GameManager.Instance.playerTurn.SetActive(false);
		GameManager.Instance.ExecuteCoroutine(NextTurn());
	}

	public IEnumerator NextTurn()
    {
		// Update the sibling index variable
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(HandState))
			{
				card.UpdateSiblingIndex();
			}
		}

		// Step 1: Apply effects
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				card.Effect(card.effectStats);
				yield return new WaitForSeconds(.2f);
			}
		}

		// Step 2: Calculate damage done by cards
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				currentEnemy.TakeDamage(card.attack);
				card.energy -= 1;
				yield return new WaitForSeconds(.2f);
			}
		}

		// Step 3: Check if cards died
		CheckIfCardsDied();
		if (currentEnemy == null) yield return null;

		yield return new WaitForSeconds(.2f);

		if (currentEnemy.health <= 0)
		{ // Check if enemy is dead
			NextEnemy();
		}

		yield return new WaitForSeconds(.2f);

		SetState(new EnemyTurnState());
		yield return null;
	}

	public void CheckIfCardsDied()
	{
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				if (card.energy <= 0)
				{
					card.SetState(new DeathState(card));
				}
			}
		}
		CheckIfLost();
	}

	public void CheckIfLost()
	{
		foreach (Card card in cards)
		{
			if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.energy > 0).Count() <= 0)
			{
				Debug.Log("You lose!");
				GameManager.Instance.lose.SetActive(true);
				GameManager.Instance.button.SetActive(true);
				return;
			}
		}
	}
}