using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemyTurnState : State
{
	private Enemy enemy;
	private float startPosY;
	private float endPosY;

	public override IEnumerator Start()
	{
		Debug.Log("EnemyTurnState");
		enemy = GameManager.Instance.battleManager.currentEnemy;
		startPosY = enemy.rt.localPosition.y;
		endPosY = startPosY - 100;

		// Display it's the enemies turn
		GameManager.Instance.enemyTurn.SetActive(true);

		// Attack
		enemy.Attack();
		yield return null;
	}

    public override IEnumerator Attack()
    {
		// Simulate animation for 1 second
		yield return new WaitForSeconds(1f);

		enemy.myCanvas.sortingOrder = 200;

		if (enemy.staggerCounter != 0)
		{
			enemy.staggerCounter -= 1;
			for (int i = 0; i < enemy.attackDamage.Count; i++)
			{// Check if all cards are dead, otherwise make them take damage
				GameManager.Instance.battleManager.CheckIfLost();
				if (GameManager.Instance.battleManager.cards.Count == 0) yield return null;

				enemy.attacking = true;
				yield return new WaitForSeconds(.3f);
				GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.energy > 0).OrderBy(x => x.energy).First().TakeDamage(enemy.attackDamage[i]);
				enemy.attacking = false;
				yield return new WaitForSeconds(.5f);
			}
			GameManager.Instance.battleManager.CheckIfCardsDied();
			yield return new WaitForSeconds(.2f);
		}
		else if (enemy.staggerCounter == 0)
		{
			enemy.staggerCounter = enemy.staggerTotal;
			yield return new WaitForSeconds(1f);
		}

		enemy.myCanvas.sortingOrder = 0;
		GameManager.Instance.battleManager.SetState(new PlayerTurnState());
	}

	public override IEnumerator Update()
	{
		if (!enemy.attacking)
		{
			if (enemy.rt.localPosition.y > startPosY - 0.001 && enemy.rt.localPosition.y < startPosY + 0.001)
			{
				enemy.rt.localPosition = new Vector3(enemy.rt.localPosition.x, startPosY, enemy.rt.localPosition.z);
				yield break;
			}
			enemy.rt.localPosition = Vector3.Lerp(enemy.rt.localPosition, new Vector3(enemy.rt.localPosition.x, startPosY, enemy.rt.localPosition.z), enemy.animationSpeed * .75f * Time.deltaTime);
		}
		else if (enemy.attacking)
		{
			if (enemy.rt.localPosition.y > endPosY - 0.001 && enemy.rt.localPosition.y < endPosY + 0.001)
			{
				enemy.rt.localPosition = new Vector3(enemy.rt.localPosition.x, endPosY, enemy.rt.localPosition.z);
				yield break;
			}
			enemy.rt.localPosition = Vector3.Lerp(enemy.rt.localPosition, new Vector3(enemy.rt.localPosition.x, endPosY, enemy.rt.localPosition.z), enemy.animationSpeed * Time.deltaTime);
		}
	}
}