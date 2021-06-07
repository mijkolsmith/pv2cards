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

		card.transform.SetParent(GameManager.Instance.arenaPanel.transform, true);

		yield return null;
	}

	public override IEnumerator Update()
	{
		//TODO: the text is weird and unreadable, card should lay differently, mouse hover maybe possibility
		card.transform.localScale = new Vector3(1,1,1);
		card.transform.localPosition = new Vector3(card.transform.localPosition.x, card.transform.localPosition.y, 0);
		card.transform.localRotation = Quaternion.identity;
		yield return null;
	}
}