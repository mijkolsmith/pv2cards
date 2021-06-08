using System.Collections;
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
				GameManager.Instance.battleManager.cards[i].SetState(new HandState(GameManager.Instance.battleManager.cards[i]));
			}
		}
        else if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).Count() != 0)
        {
			Card card = GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)).First();
			card.StartCoroutine(card.SlowToHand());
		}

		// Update visuals
		foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState)))
		{
			card.UpdateSiblingIndex();
		}

		foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
		{
			card.posSet = false;
		}

		// Display it's the player's turn
		GameManager.Instance.battleManager.playerMove = true;

		yield return null;
	}

	public override IEnumerator Exit()
	{
		GameManager.Instance.battleManager.playerMove = false;
		yield return null;
	}
}