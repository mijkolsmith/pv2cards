using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ArenaState : State
{
	private Card card;
	public ArenaState(Card card)
	{ this.card = card; }

	// If I set Pos to y = 0 it warps to y = -112.5... no idea why, but +112.5f offset works for now
	private float startPosY = 112.5f;
	private float endPosY = 400f;
	private bool firstFramePassed;

	public override IEnumerator Start()
	{
		GameManager.Instance.arenaPanel.GetComponent<AudioSource>().Play();
		Debug.Log(card.name + ": In Arena");
		Transform location = GameManager.Instance.arenaPanel.transform.GetComponentsInChildren<CanvasRenderer>().Where(x => x.transform.childCount == 1 && x.gameObject.GetComponentInChildren<HoverCheck>().mouseHover == true).FirstOrDefault().transform;
		card.rt.SetParent(location, true);
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
		if (!firstFramePassed)
		{
			firstFramePassed = true;
			card.rt.localPosition = new Vector3(0, startPosY, 0);
			LayoutRebuilder.ForceRebuildLayoutImmediate(GameManager.Instance.arenaPanel.GetComponent<RectTransform>());
		}

		if (!card.attacking)
		{
			card.rt.localScale = new Vector3(1, 1, 1);
			card.rt.localRotation = Quaternion.identity;
			
			if (card.rt.localPosition.y > startPosY - 0.001 && card.rt.localPosition.y < startPosY + 0.001)
			{
				card.rt.localPosition = new Vector3(0, startPosY, 0);
				yield break;
			}
			card.rt.localPosition = Vector3.Lerp(card.rt.localPosition, new Vector3(0, startPosY, 0), card.animationSpeed * .75f * Time.deltaTime);
		}
		else if (card.attacking)
        {
			card.rt.localScale = new Vector3(1, 1, 1);
			card.rt.localRotation = Quaternion.identity;
			
			if (card.rt.localPosition.y > endPosY - 0.001 && card.rt.localPosition.y < endPosY + 0.001)
			{
				card.rt.localPosition = new Vector3(0, endPosY, 0);
				yield break;
			}
			card.rt.localPosition = Vector3.Lerp(card.rt.localPosition, new Vector3(0, endPosY, 0), card.animationSpeed * Time.deltaTime);
		}

		if (card.energy <= 0)
        {
			card.SetState(new DeathState(card));
        }
		yield return null;
	}
}