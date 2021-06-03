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
		enemyHealth.text = currentEnemy.health.ToString();
		enemyAttack.text = currentEnemy.attackDamage.ToString();
	}

	public void NextEnemy()
	{
		Debug.Log("NextEnemy");
		enemyIndex++;
		Debug.Log("enemy count: " + enemies.Count + ",   index: " + enemyIndex);
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

		// Calculate damage
		foreach(Card card in cards)
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