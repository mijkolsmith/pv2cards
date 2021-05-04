using System.Collections;
using UnityEngine;

public class EnemyTurnState : State
{
	Enemy enemy;
	public override IEnumerator Start()
	{
		Debug.Log("EnemyTurnState");
		enemy = GameManager.Instance.battleManager.currentEnemy;
		//display it's the enemies turn, attack

		//simulate animation for 1 second
		yield return new WaitForSeconds(1f);

		//attack
		enemy.Attack();
		yield return null;
	}

	public override IEnumerator Attack()
	{
		//TODO: do i need this? not sure
		enemy.Attack();

		yield return null;
	}
}