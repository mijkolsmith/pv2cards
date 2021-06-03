using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerTurnState : State
{
	Enemy enemy;
	public override IEnumerator Start()
	{
		Debug.Log("PlayerTurnState");

		if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)).Count() == 0)
        {
			for (int i = 0; i < 5; i++)
			{
				GameManager.Instance.battleManager.cards[i].SetState(new HandState(GameManager.Instance.battleManager.cards[i]));
			}
		}
		
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