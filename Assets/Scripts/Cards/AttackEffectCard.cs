using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackEffectCard : ICard
{
    // Interface implementation
    [HideInInspector] public int attack { get; set; }
    [HideInInspector] public int energy { get; set; }
    public void TakeDamage(int damage)
    {
        energy -= damage;
    }

    // Decorator
    private readonly Card card;
    public CardEffect cardEffect = CardEffect.ATTACKMODIFIER;

    public AttackEffectCard(Card card)
	{
        this.card = card;
	}

    public void Effect(EffectStats stats)
	{
        if (stats.left)
        {
            Debug.Log(card.name + "trying to add effect left");
            if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex - 1).FirstOrDefault() != null)
            {
                GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex - 1).FirstOrDefault().attack += stats.modifier;
                Debug.Log(card.name + "trying to apply effect to " + GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex + 1).FirstOrDefault());
            }
        }
        else if (!stats.left)
        {
            Debug.Log(card.name + "trying to add effect right");
            if (GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex + 1).FirstOrDefault() != null)
            {
                Debug.Log(card.name + "trying to apply effect to " + GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex + 1).FirstOrDefault());
                GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.siblingIndex == card.siblingIndex + 1).FirstOrDefault().attack += stats.modifier;
            }
        }
    }
}