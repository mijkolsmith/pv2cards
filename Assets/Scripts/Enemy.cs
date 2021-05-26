using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Enemy : IDamageable
{
	// IDamageable
	public int health;
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
			//attack a card 
			staggerCounter -= 1;

			GameManager.Instance.battleManager.cards.Where(x => x.cardState == CardState.arena).First().TakeDamage(attackDamage);

			//check if all cards are dead
			if (GameManager.Instance.battleManager.cards.Where(x => x.cardState == CardState.arena).Count() == 0)
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