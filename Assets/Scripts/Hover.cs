using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	// Edit these variables however you like :)!
	private readonly float endPosHeight = 50; // How high the card will be when hovering over it
	private readonly float endRot = 0; // How much the card should be tilted when hovering over it
	private readonly float endScale = 1.7f; // How big the card will be when hovering over it
	private readonly float animationSpeed = 5; // How fast the card will move when hovering over it
	private readonly int width = 150; // The width of the card
	private readonly int height = 225; // The height of the card

	// Other variables needed for the script to work
	private int siblingIndex;
	private Vector3 startPos;
	private Vector3 endPos;
	private float startRot;
	private float startScale;
	private bool mouseHover = false;

	private void Start()
	{
		// Apply the width and height
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(width, height);

		// Set some starting variables for later use
		siblingIndex = transform.GetSiblingIndex();
		endPos = new Vector3(transform.position.x, transform.position.y + endPosHeight, transform.position.z);
		startPos = transform.position;
		startRot = transform.rotation.eulerAngles.z;
		startScale = transform.localScale.x;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{// Play the card if someone releases Mouse0 when the card is hovering above endPos.y + endPosHeight
			if (transform.position.y > endPos.y + endPosHeight)
			{
				//TODO: When a card is released above endPos.y + endPosHeight, use the card
				if (GameManager.Instance.battleManager.playerMove == true)
				{
					Debug.Log("The Card " + gameObject.name + " is played");
					GameManager.Instance.battleManager.PlayCard(gameObject.GetComponent<Card>());
				}
				else { Debug.Log("it's not your turn"); }
			}
		}
		if (mouseHover && Input.GetKey(KeyCode.Mouse0))
		{// Drag the card whenever Mouse0 is pressed
			transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - height / 2, Input.mousePosition.z);
		}
		if (mouseHover)
		{// Animate the card when hovering over it
			// If the Lerp is close enough, don't bother lerping anymore
			if (transform.position.y < endPos.y)
			{
				if (transform.position.y > endPos.y - 0.001 && transform.localScale.x > endScale - 0.001 && transform.rotation.eulerAngles.z > endRot - 0.001)
				{ return; }
			}

			// Lerp transform.position to endPos
			transform.position = Vector3.Lerp(transform.position, endPos, animationSpeed * Time.deltaTime);
			
			// Lerp transform.localScale to endScale
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, endScale, animationSpeed * Time.deltaTime));
			
			// Lerp transform.rotation.z to endRot
			transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, endRot), animationSpeed * Time.deltaTime);
		}
		if (!mouseHover)
		{
			// If the Lerp is close enough, don't bother lerping anymore
			if (transform.position.y < startPos.y + 0.01 && transform.localScale.x < startScale + 0.01 && transform.rotation.eulerAngles.z < startRot + 0.01)
			{ return; }

			// Reset position with Lerp
			transform.position = Vector3.Lerp(transform.position, startPos, animationSpeed * Time.deltaTime);

			// Reset localScale with Lerp
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, startScale, animationSpeed * Time.deltaTime));

			// Reset rotation with Lerp
			transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, startRot), animationSpeed * Time.deltaTime);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartCoroutine(slowSibling());
		mouseHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.SetSiblingIndex(siblingIndex);
		mouseHover = false;
	}

	IEnumerator slowSibling()
	{
		yield return new WaitForSeconds(0.2f);
		if (mouseHover)
		{
			transform.SetAsLastSibling();
		}
	}
}