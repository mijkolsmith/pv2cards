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
		Debug.Log(card.name + ": In Arena");

		// TODO: there's a bug it becomes small?
		card.transform.SetParent(GameManager.Instance.arenaPanel.transform, true);

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