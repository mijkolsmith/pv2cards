using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerTurnState : State
{
	Enemy enemy;
	public override IEnumerator Start()
	{
		// Draw 5 cards at the start, otherwise draw 1 card
		if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)).Count() == 0)
        {
			for (int i = 0; i < 5; i++)
			{
				List<Card> drawableCards = GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).ToList();
				int x = Random.Range(0, drawableCards.Count);
				if (drawableCards.ElementAtOrDefault(x) != null)
				{
					drawableCards[x].SetState(new HandState(drawableCards[x]));
				}
			}
		}
        else if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).Count() != 0)
        {
			Card card = GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).ElementAtOrDefault(Random.Range(0, GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).ToList().Count));
			card.StartCoroutine(card.SlowToHand());
		}
		else if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).Count() == 0)
		{
			GameManager.Instance.deckBack.SetActive(false);
		}

		// Update visuals
		foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
		{
			card.UpdateSiblingIndex();
		}

		foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
		{
			card.posSet = false;
		}

		// Display it's the player's turn
		GameManager.Instance.battleManager.playerMove = true;
		GameManager.Instance.endTurn.SetActive(true);

		yield return null;
	}

	public override IEnumerator Exit()
	{
		GameManager.Instance.battleManager.playerMove = false;
		yield return null;
	}
}