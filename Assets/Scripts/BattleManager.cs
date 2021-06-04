using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BattleManager : StateMachine
{
	List<Enemy> enemies = new List<Enemy>();
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
		GenerateEnemies(5);
		currentEnemy = enemies[enemyIndex];

		//TODO: need to read this from the scene prefabs instead like the card instead of generating enemies
		//enemyHealth.text = currentEnemy.health.ToString();
		//enemyAttack.text = currentEnemy.attackDamage.ToString();
	}

	public void NextEnemy()
	{
		Debug.Log("enemy " + enemyIndex + " died");
		enemyIndex++;
		if (enemies.Count >= enemyIndex)
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
		GenerateEnemies(5);
		currentEnemy = enemies[enemyIndex];
		SetState(new MenuState());
	}

	public void GenerateEnemies(int amountOfEnemies)
	{
		for (int i = 0; i < amountOfEnemies; i++)
		{
			enemies.Add(new TestEnemy(new List<int>() { Random.Range(1, 2) }, 1, Random.Range(1, 10), Random.Range(1, 5))); ;
		}
	}

	public void PlayCard(Card playedCard)
    {
		// Put the card on the board
		playedCard.SetState(new ArenaState(playedCard));

		NextTurn();
	}

	public void NextTurn()
    {
		// Calculate damage
		foreach (Card card in cards)
		{
			//needs testing
			if (card.GetState().GetType() == typeof(ArenaState))
			{
				currentEnemy.TakeDamage(card.attack);
				card.energy -= 1;
				if (card.energy <= 0)
				{
					card.SetState(new DeathState(card));
				}
			}
		}

		if (currentEnemy.health <= 0)
		{ // Check if enemy is dead
			NextEnemy();
		}

		SetState(new EnemyTurnState());
	}
}