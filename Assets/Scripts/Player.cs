using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public void UseCard(Card card)
	{
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