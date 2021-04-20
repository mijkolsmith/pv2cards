using System.Collections;

public class EnemyState : State
{
	public override IEnumerator Start()
	{
		//display it's the enemies turn, attack
		yield return null;
	}

	public override IEnumerator Attack()
	{
		//calculate damage, set state to playerturn if health > 0, else: cutscene/next enemy
		yield return null;
	}
}