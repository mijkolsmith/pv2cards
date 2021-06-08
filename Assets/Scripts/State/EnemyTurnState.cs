using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemyTurnState : State
{
	Enemy enemy;
	public override IEnumerator Start()
	{
		Debug.Log("EnemyTurnState");
		enemy = GameManager.Instance.battleManager.currentEnemy;

		//display it's the enemies turn, attack
		GameManager.Instance.endTurn.SetActive(false);

		//simulate animation for 1 second
		yield return new WaitForSeconds(1f);

		//attack
		enemy.Attack();
		yield return null;
	}

    public override IEnumerator Attack()
    {
		if (enemy.staggerCounter != 0)
		{
			enemy.staggerCounter -= 1;
			for (int i = 0; i < enemy.attackDamage.Count; i++)
			{// Check if all cards are dead, otherwise make them take damage
				GameManager.Instance.battleManager.CheckIfLost();
				if (GameManager.Instance.battleManager.cards.Count == 0) yield return null;

				yield return new WaitForSeconds(.5f);
				GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.energy > 0).OrderBy(x => x.energy).First().TakeDamage(enemy.attackDamage[i]);
			}
			GameManager.Instance.battleManager.CheckIfCardsDied();
		}
		else if (enemy.staggerCounter == 0)
		{
			enemy.staggerCounter = enemy.staggerTotal;
			yield return new WaitForSeconds(1f);
		}
			
		GameManager.Instance.battleManager.SetState(new PlayerTurnState());
		yield return null;
	}
}