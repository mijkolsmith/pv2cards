using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsLeft : MonoBehaviour
{
    HoverCheck hover;
    bool mouseHover;
    TextMeshProUGUI tmp;
    void Start()
    {
        hover = GetComponent<HoverCheck>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        mouseHover = hover.mouseHover;
        if (mouseHover)
		{
            tmp.text = "Cards left: " + (gameObject.transform.parent.parent.childCount - 1).ToString();
		}
        if (!mouseHover)
        {
            tmp.text = "";
        }
    }
}