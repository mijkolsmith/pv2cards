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

	public List<int> attackDamage = new List<int>();
	public int staggerTotal;
	public int staggerCounter;

	public void Attack()
	{
		if (staggerCounter != 0)
		{
			//attack a card 
			staggerCounter -= 1;

			//check if all cards are dead
			if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState)).Count() > 0)
			{
                for (int i = 0; i < attackDamage.Count; i++)
                {
					GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState)).OrderBy(x => x.energy).First().TakeDamage(attackDamage[i]);
				}
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