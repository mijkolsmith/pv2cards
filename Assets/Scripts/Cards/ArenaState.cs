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
		Debug.Log(card.name + ": In Arena");
		//TODO: implement new hovercheck so cards know which card is left or right of them
		Transform location = GameManager.Instance.arenaPanel.transform.GetComponentsInChildren<CanvasRenderer>().Where(x => x.transform.childCount == 1 && x.gameObject.GetComponentInChildren<HoverCheck>().mouseHover == true).FirstOrDefault().transform;
		card.transform.SetParent(location, true);

		if (card.myCanvas != null)
		{
			card.myCanvas.sortingOrder = 1;
		}

		card.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
		card.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
		card.transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

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