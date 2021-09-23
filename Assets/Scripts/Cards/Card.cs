using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class Card : StateMachine, ICard
{
    // Interface implementation
    [HideInInspector] public int attack { get; set; }
    [HideInInspector] public int energy { get; set; }
    
    public void TakeDamage(int damage)
    {
        energy -= damage;
    }

    public void Effect(EffectStats stats) 
    {
        if (effect == CardEffect.ATTACKMODIFIER)
		{
            AttackEffectCard e = new AttackEffectCard(this);
            e.Effect(stats);
		}

        if (effect == CardEffect.ENERGYMODIFIER)
        {
            EnergyEffectCard e = new EnergyEffectCard(this);
            e.Effect(stats);
        }
    }

    public CardEffect effect;
    [HideInInspector] public int modifier;
    [HideInInspector] public bool left;
    [HideInInspector] public bool multiply;
    public EffectStats effectStats;

    TextMeshProUGUI attackText;
    TextMeshProUGUI energyText;

    // Edit these variables however you like (They influence the animations)
    public readonly float endPosHeight = 150; // How high the card will be when hovering over it
    public readonly float endScale = 1.7f; // How big the card will be when hovering over it
    public readonly float animationSpeed = 5; // How fast the card will move when hovering over it
    public readonly int width = 100; // The width of the card
    public readonly int height = 225; // The height of the card

    // Other variables needed for the script to work
    [HideInInspector] public int siblingIndex;
    [HideInInspector] public bool mouseHover = false;
    [HideInInspector] private bool executed = false;
    [HideInInspector] public Canvas myCanvas;
    [HideInInspector] public bool posSet;
    [HideInInspector] public RectTransform rt;
    [HideInInspector] public bool attacking;

    public void Start()
    {
        List<TextMeshProUGUI> texts = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "Attack" || x.name == "Energy").ToList();
        attackText = texts.Where(x => x.name == "Attack").First();
        energyText = texts.Where(x => x.name == "Energy").First();
        attack = int.Parse(attackText.text);
        energy = int.Parse(energyText.text);

        SetState(new DeckState(this));
        if (GameManager.Instance.battleManager != null)
        {
            GameManager.Instance.battleManager.cards.Add(this);
        }

        // Apply the width and height
        rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);

        // Set some starting variables for later use
        siblingIndex = transform.GetSiblingIndex();
        myCanvas = GetComponent<Canvas>();
        myCanvas.sortingOrder = siblingIndex;

        // Initialize an EffectStats class
        effectStats = new EffectStats(modifier, left, multiply);
    }

    public new void SetState(State state)
    {
        if (this == null)
        {
            return;
        }
        base.SetState(state);
    }

    public new void Update()
    {
        mouseHover = GetComponent<Hover>().mouseHover;
        base.Update();
        attackText.text = attack.ToString();
        energyText.text = energy.ToString();

        if (mouseHover && !executed)
        {
            StartCoroutine(SlowChangeSortOrder());
            executed = true;
        }
        else if (!mouseHover && executed)
        {
            if (GetState().GetType() == typeof(HandState))
            {
                myCanvas.sortingOrder = siblingIndex + 5;
            }
            else
            {
                myCanvas.sortingOrder = siblingIndex;
            }
            executed = false;
        }
    }

    public void UpdateSiblingIndex()
	{
        siblingIndex = transform.GetSiblingIndex();
        if (myCanvas != null)
        {
            myCanvas.sortingOrder = siblingIndex + 5;
        }
    }

    IEnumerator SlowChangeSortOrder()
    {
        yield return new WaitForSeconds(0.2f);
        if (mouseHover)
        {
            myCanvas.sortingOrder = 200;
        }
    }

    public IEnumerator SlowToHand()
    {
        //TODO: better animation
        for (int i = 0; i < 40; i++)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - i / 2, transform.localPosition.y - i / 2, transform.localPosition.z);
            yield return new WaitForSeconds(.03f);
        }

        SetState(new HandState(this));

        foreach (Card card in GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(HandState)))
        {
            card.posSet = false;
        }
        yield return null;
    }
}