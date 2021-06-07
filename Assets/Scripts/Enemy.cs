using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
	// IDamageable
	public int health;
	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.Log(GameManager.Instance.battleManager.currentEnemy + " health: " + health);
	}

	// Enemy Variables
	//TODO: set to private after debuwug
	public List<int> attackDamage = new List<int>();
	public int staggerTotal;
	public int staggerCounter;

	// Visuals
	public Slider healthSlider;
	public List<TextMeshProUGUI> attackDamageText = new List<TextMeshProUGUI>();
	public TextMeshProUGUI staggerText;

	public void Start()
	{
		healthSlider = GetComponentInChildren<Slider>();
		healthSlider.maxValue = health;

		List<TextMeshProUGUI> texts = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "Attack" || x.name == "Stagger").ToList();
		attackDamageText = texts.Where(x => x.name == "Attack").ToList();
		staggerText = texts.Where(x => x.name == "Stagger").First();
		for (int i = 0; i < attackDamageText.Count; i++)
		{
			attackDamage.Add(int.Parse(attackDamageText[i].text));
		}
		staggerTotal = int.Parse(staggerText.text);
		staggerCounter = staggerTotal;

		if (GameManager.Instance.battleManager != null)
		{
			GameManager.Instance.battleManager.enemies.Add(this);
		}
	}

	public void Update()
	{
		staggerText.text = staggerCounter.ToString();
		healthSlider.value = health;
	}

	public void Attack()
	{
		if (staggerCounter != 0)
		{
			staggerCounter -= 1;

			
			for (int i = 0; i < attackDamage.Count; i++)
			{// Check if all cards are dead, otherwise make them take damage
				GameManager.Instance.battleManager.CheckIfLost();
				GameManager.Instance.battleManager.cards.Where(x => x.GetState().GetType() == typeof(ArenaState) && x.energy > 0).OrderBy(x => x.energy).First().TakeDamage(attackDamage[i]);
			}

			GameManager.Instance.battleManager.CheckIfCardsDied();
			GameManager.Instance.battleManager.SetState(new PlayerTurnState());
			
		}
		else if (staggerCounter == 0)
		{
			Debug.Log("enemy staggers!");
			staggerCounter = staggerTotal;
			GameManager.Instance.battleManager.SetState(new PlayerTurnState());
		}
	}
}