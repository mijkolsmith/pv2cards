using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckState : State
{
	Card card;
	public DeckState(Card card)
	{ this.card = card; }

	public override IEnumerator Start()
	{
		card.transform.localPosition = new Vector3(1075, 500, 0);
		yield return null;
	}
}