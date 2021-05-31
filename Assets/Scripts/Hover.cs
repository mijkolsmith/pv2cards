using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	// Edit these variables however you like :)!
	private readonly float endPosHeight = 50; // How high the card will be when hovering over it
	private readonly float endScale = 1.7f; // How big the card will be when hovering over it
	private readonly float animationSpeed = 5; // How fast the card will move when hovering over it
	private readonly int width = 150; // The width of the card
	private readonly int height = 225; // The height of the card

	// Other variables needed for the script to work
	private int siblingIndex;
	private Vector3 startPos;
	private Vector3 endPos;
	private float startScale;
	private bool mouseHover = false;

	private Canvas myCanvas;
	private Card card;

	bool posSet = false;
	bool firstFramePassed = false;

	private void Start()
	{
		card = GetComponent<Card>();

		// Apply the width and height
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(width, height);

		// Set some starting variables for later use
		siblingIndex = transform.GetSiblingIndex();
		startScale = transform.localScale.x;
		myCanvas = GetComponent<Canvas>();
		myCanvas.sortingOrder = siblingIndex;
	}

	private void Update()
	{
		// For some reason, the localPosition is different in the first frame. This is a hardfix.
		if (!posSet && firstFramePassed)
		{
			startPos = transform.localPosition;
			endPos = new Vector3(transform.localPosition.x, transform.localPosition.y + endPosHeight, transform.localPosition.z);
			posSet = true;
		}

		if (!firstFramePassed)
		{
			firstFramePassed = true;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{// Play the card if someone releases Mouse0 when the card is hovering above endPos.y + endPosHeight
			if (transform.localPosition.y > endPos.y + endPosHeight)
			{
				//TODO: When a card is released above endPos.y + endPosHeight, use the card
				if (GameManager.Instance.battleManager.playerMove == true)
				{
					Debug.Log("The Card " + gameObject.name + " is played");
					GameManager.Instance.battleManager.PlayCard(card);
				}
				else { Debug.Log("it's not your turn"); }
			}
		}
		if (mouseHover && Input.GetKey(KeyCode.Mouse0))
		{// Drag the card whenever Mouse0 is pressed
			transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - height / 2, Input.mousePosition.z);
		}
		if (mouseHover && !Input.GetKey(KeyCode.Mouse0))
		{// Animate the card when hovering over it
			// If the Lerp is close enough, don't bother lerping anymore
			if (transform.localPosition.y < endPos.y)
			{
				if (transform.localPosition.y > endPos.y - 0.001 && transform.localScale.x > endScale - 0.001)
				{ return; }
			}

			// Lerp transform.position to endPos
			transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, animationSpeed * Time.deltaTime);
			
			// Lerp transform.localScale to endScale
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, endScale, animationSpeed * Time.deltaTime));
		}
		if (!mouseHover)
		{
			// If the Lerp is close enough, don't bother lerping anymore
			if (transform.localPosition.y < startPos.y + 0.01 && transform.localScale.x < startScale + 0.01)
			{ return; }

			// Reset position with Lerp
			transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, animationSpeed * Time.deltaTime);

			// Reset localScale with Lerp
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, startScale, animationSpeed * Time.deltaTime));
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartCoroutine(slowChangeSortOrder());
		mouseHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		myCanvas.sortingOrder = siblingIndex;
		mouseHover = false;
	}

	IEnumerator slowChangeSortOrder()
	{
		yield return new WaitForSeconds(0.2f);
		if (mouseHover)
		{
			myCanvas.sortingOrder = 200;
		}
	}
}