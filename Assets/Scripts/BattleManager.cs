using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : StateMachine
{
	List<Enemy> enemies = new List<Enemy>();
	public Enemy currentEnemy;
	int enemyIndex = 0;

	public List<Card> cards = new List<Card>();

	public bool playerMove;

	public void Awake()
	{
		SetState(new MenuState());
		GenerateEnemies(5);
		currentEnemy = enemies[enemyIndex];
	}

	public void NextEnemy()
	{
		Debug.Log("NextEnemy");
		enemyIndex++;
		Debug.Log("enemy count: " + enemies.Count + ",   index:" + enemyIndex);
		if (enemies.Count > enemyIndex)
		{
			currentEnemy = enemies[enemyIndex];
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
	{//TODO: enemy generation process
		for (int i = 0; i < amountOfEnemies; i++)
		{
			enemies.Add(new TestEnemy(Random.Range(1, 2), Random.Range(1, 10), Random.Range(1, 5)));
		}
	}

	public void PlayCard(Card playedCard)
    {
		// Put the card on the board
		playedCard.SetState(new ArenaState(playedCard));

		// Calculate damage
		foreach(Card card in cards)
        {
			//needs testing
			if (card.GetState().GetType() == typeof(ArenaState))
            {
				currentEnemy.TakeDamage(card.attack);
				card.energy -= 1;
            }
        }

		// Check if enemy is dead
		if (currentEnemy.health > 0)
		{
			SetState(new EnemyTurnState());
			playerMove = false;
		}
		else
		{
			NextEnemy();
		}
	}
}