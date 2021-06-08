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
	}

	// Enemy Variables
	// TODO: set to private after debuwug
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
		StartCoroutine(GameManager.Instance.battleManager.GetState().Attack());
	}
}