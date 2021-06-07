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
	public TextMeshProUGUI enemyHealth;
	public TextMeshProUGUI enemyAttack;
	public TextMeshProUGUI enemyAttackTimes;
	int enemyIndex = 0;

	public bool playerMove;

	public void Awake()
	{
		SetState(new MenuState());
	}

	public void NextEnemy()
	{
		Debug.Log("enemy " + enemyIndex + " died");
		enemyIndex++;
		if (enemies.Count > enemyIndex)
		{
			currentEnemy = enemies[enemyIndex];
			Debug.Log("new enemy " + enemyIndex + "   health: " + currentEnemy.health + "  attack damage 1: " + currentEnemy.attackDamage[0] + " stagger: " + currentEnemy.staggerTotal);
		}
		else
		{
			Debug.Log("You win!");
			GameManager.Instance.ResetGame();
		}
	}

	public void ResetGame()
	{
		enemyIndex = 0;
		enemies.Clear();
		SetState(new MenuState());
	}

	public void PlayCard(Card playedCard)
    {
		if (currentEnemy == null)
		{
			currentEnemy = enemies[enemyIndex];
		}

		// Put the card on the board
		playedCard.SetState(new ArenaState(playedCard));

		// Changes in Hand & Arena: Update siblingIndex
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(HandState) || card.GetState().GetType() == typeof(ArenaState))
			{
				card.UpdateSiblingIndex();
			}
		}

		NextTurn();
	}

	public void NextTurn()
    {
		// Step 1: Apply effects
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				card.Effect(card.effectStats);
			}
		}

		// Step 2: Calculate damage done by cards
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				currentEnemy.TakeDamage(card.attack);
				card.energy -= 1;
			}
		}

		// Step 3: Check if cards died
		CheckIfCardsDied();

		if (currentEnemy.health <= 0)
		{ // Check if enemy is dead
			NextEnemy();
		}

		SetState(new EnemyTurnState());
	}

	public void CheckIfCardsDied()
	{
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				if (card.energy <= 0)
				{
					StartCoroutine("SlowDie", card);
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
				GameManager.Instance.ResetGame();
			}
		}
	}

	IEnumerator SlowDie(Card deadCard)
    {
		yield return new WaitForSeconds(.5f);

		deadCard.SetState(new DeathState(deadCard));
		foreach (Card card in cards)
		{
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				card.UpdateSiblingIndex();
			}
		}
	}
}