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
		Debug.Log(card.name + ": In Deck");
		
		yield return null;
	}
}