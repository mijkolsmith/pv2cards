using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public TestEnemy(int attackDamage, int health, int staggerTotal)
    {
		this.attackDamage = attackDamage;
		this.health = health;
		this.staggerTotal = staggerTotal;
		staggerCounter = staggerTotal;
	}
}