using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IDamageable
{
	//IDamageable
	public int health { get; set; } = 10;
	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.Log("Player health: " + health);
	}

	public void UseCard()
	{
		//TODO: implement cards
		GameManager.Instance.battleManager.currentEnemy.TakeDamage(2);

		// Check if enemy is dead
		if (GameManager.Instance.battleManager.currentEnemy.health > 0)
		{
			GameManager.Instance.battleManager.SetState(new EnemyTurnState());
			GameManager.Instance.battleManager.playerMove = false;
		}
		else
		{
			GameManager.Instance.battleManager.NextEnemy();
		}
	}
}