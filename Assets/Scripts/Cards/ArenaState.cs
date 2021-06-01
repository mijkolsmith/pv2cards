using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaState : State
{
	Card card;
	public ArenaState(Card card)
	{ this.card = card; }

	public override IEnumerator Start()
	{
		//add card to arenaPanel
		yield return null;
	}

	public override IEnumerator Update()
	{
		yield return null;
	}

	public override IEnumerator Attack()
	{
		yield return null;
	}
}