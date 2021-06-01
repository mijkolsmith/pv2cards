using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
	Card card;
	public DeathState(Card card)
	{ this.card = card; }

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Attack()
	{
		yield return null;
	}
}