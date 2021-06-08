using System.Collections;
using UnityEngine;

public class CutsceneState : State
{
	public override IEnumerator Start()
	{
		// Play a cutscene
		Debug.Log("Cutscene start");
		yield return new WaitForSeconds(1f);
		GameManager.Instance.battleManager.SetState(new PlayerTurnState());
		yield return null;
	}
}