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
		Debug.Log(card.name + ": Death");
		Object.Destroy(card.gameObject);
		Canvas.ForceUpdateCanvases();
		yield return null;
	}
}