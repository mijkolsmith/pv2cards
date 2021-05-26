using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : StateMachine
{
	List<Enemy> enemies = new List<Enemy>();
	public Enemy currentEnemy;
	int enemyIndex = 0;

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

	public void PlayCard(Card card)
    {
		//Animate the card so it goes to the board

    }
}