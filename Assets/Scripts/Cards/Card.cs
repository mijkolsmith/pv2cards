using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : StateMachine, IDamageable
{
    public int attack;
    TextMeshProUGUI attackText;
    public int energy;
    TextMeshProUGUI energyText;

    // Edit these variables however you like :)!
    public readonly float endPosHeight = 50; // How high the card will be when hovering over it
    public readonly float endScale = 1.7f; // How big the card will be when hovering over it
    public readonly float animationSpeed = 5; // How fast the card will move when hovering over it
    public readonly int width = 150; // The width of the card
    public readonly int height = 225; // The height of the card

    // Other variables needed for the script to work
    public int siblingIndex;
    public Vector3 startPos;
    public Vector3 endPos;
    public float startScale;
    public bool mouseHover = false;

    public Canvas myCanvas;
    public GameObject handPanel;
    public GameObject arenaPanel;

    public void Start()
    {
        SetState(new DeckState(this));
        GameManager.Instance.battleManager.cards.Add(this);

        handPanel = GameManager.Instance.handPanel;
        arenaPanel = GameManager.Instance.arenaPanel;

        // Apply the width and height
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);

        // TODO: HandState
        // Set some starting variables for later use
        siblingIndex = transform.GetSiblingIndex();
        startScale = transform.localScale.x;
        myCanvas = GetComponent<Canvas>();
        myCanvas.sortingOrder = siblingIndex;
    }

    public void TakeDamage(int damage)
    {
        energy -= damage;
    }

    public new void Update()
    {
        base.Update();
        attackText.text = attack.ToString();
        energyText.text = energy.ToString();
    }
}