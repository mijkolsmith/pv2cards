using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : IDamageable
{
    public int attack;
    TextMeshProUGUI attackText;
    public int energy;
    TextMeshProUGUI energyText;

    public void TakeDamage(int damage)
    {
        energy -= damage;
    }

    public void Update()
    {
        attackText.text = attack.ToString();
        energyText.text = energy.ToString();
    }
}