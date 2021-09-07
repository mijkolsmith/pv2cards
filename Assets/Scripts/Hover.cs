using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool mouseHover = false;

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseHover = false;
	}
}