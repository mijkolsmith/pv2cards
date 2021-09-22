using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaState : State
{
	Card card;
	public ArenaState(Card card)
	{ this.card = card; }

	public override IEnumerator Start()
	{
		GameManager.Instance.arenaPanel.GetComponent<AudioSource>().Play();
		Debug.Log(card.name + ": In Arena");
		Transform location = GameManager.Instance.arenaPanel.transform.GetComponentsInChildren<CanvasRenderer>().Where(x => x.transform.childCount == 1 && x.gameObject.GetComponentInChildren<HoverCheck>().mouseHover == true).FirstOrDefault().transform;
		card.transform.SetParent(location, true);
		card.siblingIndex = location.GetSiblingIndex();

		if (card.myCanvas != null)
		{
			card.myCanvas.sortingOrder = 1;
		}

		card.rt.anchorMin = new Vector2(0.5f, 0.5f);
		card.rt.anchorMax = new Vector2(0.5f, 0.5f);
		card.rt.pivot = new Vector2(0.5f, 0.5f);

		yield return null;
	}

	public override IEnumerator Update()
	{
		card.transform.localScale = new Vector3(1,1,1);
		// If i set localPos to Vec3(0,0,0) it warps to -112.5 y... no idea why, but this works for now
		card.transform.localPosition = new Vector3(0, 112.5f, 0);
		card.transform.localRotation = Quaternion.identity;

		if (card.energy <= 0)
        {
			card.SetState(new DeathState(card));
        }
		yield return null;
	}
}