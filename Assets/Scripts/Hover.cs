using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//Edit these variables however you like :)!
	private readonly float endPos = 50;
	private readonly float endRot = 0;
	private readonly float endScale = 1.7f;
	private readonly float animationSpeed = 5;

	private int siblingIndex;
	private float startPos;
	private float startRot;
	private float startScale;
	private bool mouseHover = false;

	private void Start()
	{
		siblingIndex = transform.GetSiblingIndex();
		startPos = transform.position.y;
		startRot = transform.rotation.eulerAngles.z;
		startScale = transform.localScale.x;
	}

	private void Update()
	{
		if (mouseHover)
		{
			Debug.Log("mouseHover");

			//if the Lerp is close enough, don't bother lerping anymore
			if (transform.position.y > endPos - 0.001 && transform.localScale.x > endScale - 0.001 && transform.rotation.eulerAngles.z > endRot - 0.001)
			{ return; }

			//Lerp transform.position.y to endPos
			transform.position = new Vector3(transform.position.x,
												Mathf.Lerp(transform.position.y, endPos, animationSpeed * Time.deltaTime),
												transform.position.z);
			
			//Lerp transform.localScale to endScale
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, endScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, endScale, animationSpeed * Time.deltaTime));
			
			//Lerp transform.rotation.z to endRot
			transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, endRot), animationSpeed * Time.deltaTime);
		}
		if (!mouseHover && (transform.position.y != startPos || transform.rotation.eulerAngles.z != startRot))
		{
			//if the lerp is close enough, don't bother lerping anymore
			if (transform.position.y < startPos + 0.01 && transform.localScale.x < startScale + 0.01 && transform.rotation.eulerAngles.z < startRot + 0.01)
			{ return; }

			//Reset position with lerp
			transform.position = new Vector3(transform.position.x,
											Mathf.Lerp(transform.position.y, startPos, animationSpeed * Time.deltaTime),
											transform.position.z);

			//reset localScale with lerp
			transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.y, startScale, animationSpeed * Time.deltaTime),
												Mathf.Lerp(transform.localScale.z, startScale, animationSpeed * Time.deltaTime));

			//reset rotation with lerp
			transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, startRot), animationSpeed * Time.deltaTime);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("mouseEnter");
		StartCoroutine(slowSibling());
		mouseHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("mouseExit");
		transform.SetSiblingIndex(siblingIndex);
		mouseHover = false;
	}

	IEnumerator slowSibling()
	{
		yield return new WaitForSeconds(0.2f);
		if (mouseHover)
		{ transform.SetAsLastSibling(); }
	}
}