using System.Collections;

public class AttackState : State
{
	public override IEnumerator Start()
	{
		//display it's the player's turn
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