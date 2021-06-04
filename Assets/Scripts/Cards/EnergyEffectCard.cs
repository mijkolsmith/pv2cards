using UnityEngine;
using System.Linq;

public class EnergyEffectCard : ICard
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
    public CardEffect cardEffect = CardEffect.ENERGYMODIFIER;

    public EnergyEffectCard(Card card)
	{
        this.card = card;
	}

    public void Effect(EffectStats stats)
	{
        if (stats.left)
        {
            GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState) && x.siblingIndex == card.siblingIndex - 1).FirstOrDefault().energy += stats.modifier;
        }
        else if (!stats.left)
        {
            GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(DeckState) && x.siblingIndex == card.siblingIndex + 1).FirstOrDefault().energy += stats.modifier;
        }
    }
}