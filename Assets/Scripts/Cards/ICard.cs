using UnityEngine;
using TMPro;

public interface ICard : IDamageable
{
    [HideInInspector] public int attack { get; set; }
    [HideInInspector] public int energy { get; set; }

    public void Effect(EffectStats stats);
}