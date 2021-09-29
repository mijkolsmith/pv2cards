using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandState : State
{
	Card card;
	public HandState(Card card)
	{ this.card = card; }

	private Vector3 startPos;
	private Vector3 endPos;

	public float startScale;
	private bool firstFramePassed;
	private bool hovering = false;

	public override IEnumerator Start()
	{
		Debug.Log(card.name + ": In Hand ");

		startScale = card.transform.localScale.x;
		card.transform.SetParent(GameManager.Instance.handPanel.transform);
		card.rt.pivot = new Vector2(0.5f, 0f);

		card.siblingIndex = card.transform.GetSiblingIndex();
		if (card.myCanvas != null)
		{
			card.myCanvas.sortingOrder = card.siblingIndex + 5;
		}

		yield return null;
	}

	public override IEnumerator Update()
	{
		if (!card.posSet && firstFramePassed)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(GameManager.Instance.handPanel.GetComponent<RectTransform>());
			startPos = new Vector3(card.rt.localPosition.x, 0, card.rt.localPosition.z);
			endPos = new Vector3(card.rt.localPosition.x, card.rt.localPosition.y + card.endPosHeight, card.rt.localPosition.z);
			card.posSet = true;
		}

		if (!firstFramePassed)
		{
			firstFramePassed = true;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{// Play the card if someone releases Mouse0 when the card is above an empty slot
			if (GameManager.Instance.battleManager.playerMove == true && hovering)
			{
				GameObject locationObject = (GameManager.Instance.arenaPanel.transform.GetComponentsInChildren<CanvasRenderer>().Where(x => x.transform.childCount == 1 && x.gameObject.GetComponentInChildren<HoverCheck>().mouseHover == true).FirstOrDefault().gameObject == null) ? null : GameManager.Instance.arenaPanel.transform.GetComponentsInChildren<CanvasRenderer>().Where(x => x.transform.childCount == 1 && x.gameObject.GetComponentInChildren<HoverCheck>().mouseHover == true).FirstOrDefault().gameObject;
				if (locationObject != null)
				{
					GameManager.Instance.battleManager.playerMove = false;
					GameManager.Instance.StartCoroutine(GameManager.Instance.battleManager.PlayCard(card,locationObject.transform));
					
					foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
					{
						card.posSet = false;
					}
				}
				
			}
			else { Debug.Log("it's not your turn"); }

			hovering = false;
		}

		if (card.mouseHover && Input.GetKey(KeyCode.Mouse0) && GameManager.Instance.tutorialClosed == true)
		{// Drag the card whenever Mouse0 is pressed
			card.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - card.height / 2, Input.mousePosition.z);
			hovering = true;
		}
		if (card.mouseHover && !Input.GetKey(KeyCode.Mouse0))
		{// Animate the card when hovering over it
			// If the Lerp is close enough, don't bother lerping anymore
			if (card.rt.localPosition.y > endPos.y - 0.001 && card.rt.localPosition.y < endPos.y + 0.001 &&
				card.rt.localScale.x > card.endScale - 0.001 && card.rt.localScale.x < card.endScale + 0.001)
			{
				yield break; 
			}

			// Lerp transform.position to endPos
			card.rt.localPosition = Vector3.Lerp(card.rt.localPosition, endPos, card.animationSpeed * Time.deltaTime);

			// Lerp card.rectTransform.localScale to endScale
			card.rt.localScale = Vector3.Lerp(card.rt.localScale, Vector3.one * card.endScale, card.animationSpeed * Time.deltaTime);
		}

		if (!card.mouseHover)
		{
			// If the Lerp is close enough, don't bother lerping anymore
			if (card.rt.localPosition.y > startPos.y - 0.001f && card.rt.localPosition.y < startPos.y + 0.001f &&
				card.rt.localScale.x > startScale - 0.001f && card.rt.localScale.x < startScale + 0.001f)
			{
				card.rt.localPosition = new Vector3(card.rt.localPosition.x, startPos.y, card.rt.localPosition.z);
				card.rt.localScale = Vector3.one * startScale;
				yield break; 
			}

			hovering = false;

			// Reset position with Lerp
			card.rt.localPosition = Vector3.Lerp(card.rt.localPosition, startPos, card.animationSpeed * Time.deltaTime);

			// Reset localScale with Lerp
			card.rt.localScale = Vector3.Lerp(card.rt.localScale, Vector3.one * startScale, card.animationSpeed * Time.deltaTime);
		}

		yield return null;
	}
}