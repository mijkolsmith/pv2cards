using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathState : State
{
	Card card;
	public DeathState(Card card)
	{ this.card = card; }

	public override IEnumerator Start()
	{
		List<Image> images = card.GetComponentsInChildren<Image>().ToList();
		List<TextMeshProUGUI> texts = card.GetComponentsInChildren<TextMeshProUGUI>().ToList();

		// Death fade animation
		for (int i = 0; i < 122; i++)
        {
			foreach (var image in images)
            {
				image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 1f/122f);
            }
			foreach (var text in texts)
			{
				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 1f/122f);
			}
			yield return new WaitForSeconds(.01f);
		}

		Debug.Log(card.name + ": Death");
		Object.Destroy(card.gameObject);
		Canvas.ForceUpdateCanvases();
		yield return null;
	}
}