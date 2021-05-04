using System.Collections;
using UnityEngine;

public class PlayerTurnState : State
{
	Enemy enemy;
	public override IEnumerator Start()
	{
		Debug.Log("PlayerTurnState");
		//display it's the player's turn
		GameManager.Instance.battleManager.playerMove = true;
		yield return null;
	}

	public override IEnumerator Update()
	{
		//check for input, player chooses card
		//when input use card
		yield return null;
	}

	public override IEnumerator Attack()
	{
		//use card, calculations, set state to enemyturn
		
		yield return null;
	}
}