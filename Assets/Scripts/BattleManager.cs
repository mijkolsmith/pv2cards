using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

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

	public IEnumerator NextEnemy()
	{
		if (currentEnemy != null)
		{
			GameManager.Instance.ExecuteCoroutine(currentEnemy.Die());
			enemies.Remove(currentEnemy);

			yield return new WaitForSeconds(1f);
		}

		if (enemies.Where(x => x.health > 0).Count() <= 0)
        {
			Debug.Log("You win!");
			GameManager.Instance.win.gameObject.SetActive(true);
			GameManager.Instance.button.SetActive(true);
			yield break;
		}
		
		currentEnemy = enemies.Where(x => x != null).OrderBy(x => x.health).FirstOrDefault();
		currentEnemy.transform.SetParent(GameManager.Instance.enemyHolder.transform);
		currentEnemy.transform.localPosition = Vector3.zero;

		SetState(new EnemyTurnState());
		Debug.Log("enemy name: " + currentEnemy.name + "   health: " + currentEnemy.health + "  attack damage 1: " + currentEnemy.attackDamage[0] + " stagger: " + currentEnemy.staggerTotal);
	}

	public void ResetGame()
	{
		SetState(new MenuState());
		cards.Clear();
		enemies.Clear();
	}

	public IEnumerator PlayCard(Card playedCard, Transform location)
    {
		if (currentEnemy == null)
		{
			currentEnemy = enemies.Where(x => x != null).OrderBy(x => x.health).FirstOrDefault();
			currentEnemy.transform.SetParent(GameManager.Instance.enemyHolder.transform);
			currentEnemy.transform.localPosition = Vector3.zero;
		}

		yield return new WaitForSeconds(.2f);

		// Put the card on the board
		playedCard.SetState(new ArenaState(playedCard, location));

		LayoutRebuilder.ForceRebuildLayoutImmediate(GameManager.Instance.handPanel.GetComponent<RectTransform>());
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(HandState))
			{
				card.UpdateSiblingIndex();
			}
		}

		yield return new WaitForSeconds(.1f);

		GameManager.Instance.playerTurn.SetActive(false);
		GameManager.Instance.ExecuteCoroutine(NextTurn());
	}

	public IEnumerator NextTurn()
    {
		// Update the sibling index variable
		foreach (Card card in cards.Where(x => x.GetState().GetType() == typeof(HandState)))
		{
			card.posSet = false;
			card.UpdateSiblingIndex();
		}

		// Step 1: Apply effects
		foreach (Card card in cards.Where(x => x.GetState().GetType() == typeof(ArenaState)))
		{
			card.Effect(card.effectStats);
			yield return new WaitForSeconds(.2f);
		}

		// Step 2: Calculate damage done by cards
		foreach (Card card in cards.Where(x => x.GetState().GetType() == typeof(ArenaState)))
		{
			card.attacking = true;
			yield return new WaitForSeconds(.1f);
			currentEnemy.TakeDamage(card.attack);
			yield return new WaitForSeconds(.1f);
			card.energy -= 1;
			yield return new WaitForSeconds(.1f);
			card.attacking = false;
		}

		// Step 3: Check if cards died
		CheckIfCardsDied();
		if (currentEnemy == null) yield return null;

		yield return new WaitForSeconds(.2f);

		if (currentEnemy.health <= 0)
		{ // Check if enemy is dead
			GameManager.Instance.ExecuteCoroutine(NextEnemy());
		}
		else
		{
			SetState(new EnemyTurnState());
		}
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