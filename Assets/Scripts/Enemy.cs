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
	public List<int> attackDamage = new List<int>();
	public int staggerTotal;
	public int staggerCounter;

	// Visuals
	public Slider healthSlider;
	public List<TextMeshProUGUI> attackDamageText = new List<TextMeshProUGUI>();
	public TextMeshProUGUI staggerText;

	public readonly float animationSpeed = 10; // How fast the enemy will move when animating
	[HideInInspector] public bool attacking = false;
	[HideInInspector] public RectTransform rt;
	[HideInInspector] public Canvas myCanvas;


	public void Start()
	{
		healthSlider = GetComponentInChildren<Slider>();
		healthSlider.maxValue = health;
		rt = GetComponent<RectTransform>();
		myCanvas = GetComponent<Canvas>();

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

	public IEnumerator Die()
	{
		List<Image> images = GetComponentsInChildren<Image>().ToList();
		List<TextMeshProUGUI> texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();

		// Death fade animation
		for (int i = 0; i < 100; i++)
		{
			foreach (var image in images)
			{
				image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 1f / 100f);
			}
			foreach (var text in texts)
			{
				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 1f / 100f);
			}
			yield return new WaitForSeconds(.01f);
		}

		Destroy(gameObject);
	}
}