using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : IDamageable
{
	// IDamageable
	public int health { get; set; }
	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.Log(GameManager.Instance.battleManager.currentEnemy + " health: " + health);
	}

	protected int attackDamage;
	protected int staggerTotal;
	protected int staggerCounter;

	public void Attack()
	{
		if (staggerCounter != 0)
		{
			GameManager.Instance.player.TakeDamage(attackDamage);
			staggerCounter -= 1;
			if (GameManager.Instance.player.health > 0)
			{
				GameManager.Instance.battleManager.SetState(new PlayerTurnState());
			}
			else
			{
				Debug.Log("You lose!");
				GameManager.Instance.ResetGame();
			}
		}
		else if (staggerCounter == 0)
		{
			Debug.Log("enemy staggers!");
			staggerCounter = staggerTotal;
			GameManager.Instance.battleManager.SetState(new PlayerTurnState());
		}
	}
}