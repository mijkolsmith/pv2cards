using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private List<Menu> menuObjects = new List<Menu>();
	private Dictionary<MenuType, Menu> menus = new Dictionary<MenuType, Menu>();
	private MenuType currentMenu;
	[SerializeField] private Image transition;

	private static MenuManager instance = null;
	public static MenuManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<MenuManager>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (instance != this)
		{
			Destroy(gameObject);
		}

		currentMenu = MenuType.Main;
		foreach (Menu m in menuObjects)
		{
			menus.Add(m.menuType, m);
		}
	}

	public void NextScene()
	{
		GameManager.Instance.StartCoroutine(GameManager.Instance.SlowLoadScene(SceneManager.GetActiveScene().buildIndex + 1, transition));
	}

	public void OpenMenu(int t)
	{
		MenuType menuType = (MenuType)t;

		if (currentMenu == menuType)
		{
			return;
		}

		menus[currentMenu].Close();
		menus[menuType].Open();
		currentMenu = menuType;
	}

	public void OpenMenu(MenuType menuType)
	{
		if (currentMenu == menuType)
		{
			return;
		}

		menus[currentMenu].Close();
		menus[menuType].Open();
		currentMenu = menuType;
	}

	public void Quit()
	{
		Application.Quit();
	}
}