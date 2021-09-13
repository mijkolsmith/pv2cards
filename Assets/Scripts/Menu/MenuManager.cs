using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
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
	}

	public void NextScene()
	{
		GameManager.Instance.StartCoroutine(GameManager.Instance.SlowLoadScene(SceneManager.GetActiveScene().buildIndex + 1, transition));
	}
	public void Quit()
	{
		Application.Quit();
	}
}