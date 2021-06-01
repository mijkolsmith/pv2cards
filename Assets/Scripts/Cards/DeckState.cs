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
		card.GetComponent<RectTransform>().localPosition = Vector3.zero;
		card.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
		card.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
		card.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
		yield return null;
	}
}