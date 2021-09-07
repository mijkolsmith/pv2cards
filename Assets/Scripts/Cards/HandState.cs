using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandState : State
{
	Card card;
	public HandState(Card card)
	{ this.card = card; }

	private Vector3 startPos;
	private Vector3 endPos;

	public float startScale;
	private bool firstFramePassed;

	public override IEnumerator Start()
	{
		Debug.Log(card.name + ": In Hand");

		startScale = card.transform.localScale.x;
		card.transform.SetParent(GameManager.Instance.handPanel.transform);

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
			startPos = card.transform.localPosition;
			endPos = new Vector3(card.transform.localPosition.x, card.transform.localPosition.y + card.endPosHeight, card.transform.localPosition.z);
			card.posSet = true;
		}

		if (!firstFramePassed)
		{
			firstFramePassed = true;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{// Play the card if someone releases Mouse0 when the card is hovering above endPos.y + endPosHeight
			//TODO: Play the card if the card hovers over an empty card
			if (card.transform.localPosition.y > 0)
			{
				// When a card is released above endPos.y + endPosHeight, use the card
				if (GameManager.Instance.battleManager.playerMove == true)
				{
					GameManager.Instance.battleManager.PlayCard(card);

					foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
                    {
						card.posSet = false;
                    }
				}
				else { Debug.Log("it's not your turn"); }
			}
		}
		if (card.mouseHover && Input.GetKey(KeyCode.Mouse0))
		{// Drag the card whenever Mouse0 is pressed
			card.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - card.height / 2, Input.mousePosition.z);
		}
		if (card.mouseHover && !Input.GetKey(KeyCode.Mouse0))
		{// Animate the card when hovering over it
		 // If the Lerp is close enough, don't bother lerping anymore
			if (card.transform.localPosition.y < endPos.y)
			{
				if (card.transform.localPosition.y > endPos.y - 0.001 && card.transform.localScale.x > card.endScale - 0.001)
				{ yield break; }
			}

			// Lerp transform.position to endPos
			card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, endPos, card.animationSpeed * Time.deltaTime);

			// Lerp card.transform.localScale to endScale
			card.transform.localScale = Vector3.Lerp(card.transform.localScale, Vector3.one * card.endScale, card.animationSpeed * Time.deltaTime);
		}
		if (!card.mouseHover)
		{
			// If the Lerp is close enough, don't bother lerping anymore
			if (card.transform.localPosition.y < startPos.y + 0.01 && card.transform.localScale.x < startScale + 0.01)
			{ yield break; }

			// Reset position with Lerp
			card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, startPos, card.animationSpeed * Time.deltaTime);

			// Reset localScale with Lerp
			card.transform.localScale = Vector3.Lerp(card.transform.localScale, Vector3.one * startScale, card.animationSpeed * Time.deltaTime);
		}

		yield return null;
	}
}