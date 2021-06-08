using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	public float volume;
	public BattleManager battleManager;

	public GameObject handPanel;
	public GameObject arenaPanel;
	public Image transition;
	public GameObject enemyHolder;
	public GameObject win;
	public GameObject lose;
	public GameObject button;
	public GameObject endTurn;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		//initialize game
		battleManager = gameObject.AddComponent<BattleManager>();
	}

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && (handPanel == null || arenaPanel == null || transition == null || win == null || lose == null))
        {
			List<CanvasRenderer> panels = FindObjectsOfType<CanvasRenderer>().Where(x => x.name == "HandPanel" || x.name == "ArenaPanel" || x.name == "CurrentEnemy" || x.name == "Win" || x.name == "Lose" || x.name == "GoToMenuButton" || x.name == "EndTurn").ToList();
			handPanel = panels.Where(x => x.name == "HandPanel").FirstOrDefault().gameObject;
			arenaPanel = panels.Where(x => x.name == "ArenaPanel").FirstOrDefault().gameObject;
			enemyHolder = panels.Where(x => x.name == "CurrentEnemy").FirstOrDefault().gameObject;
			win = panels.Where(x => x.name == "Win").FirstOrDefault().gameObject;
			lose = panels.Where(x => x.name == "Lose").FirstOrDefault().gameObject;
			button = panels.Where(x => x.name == "GoToMenuButton").FirstOrDefault().gameObject;
			endTurn = panels.Where(x => x.name == "EndTurn").FirstOrDefault().gameObject;

			win.SetActive(false);
			lose.SetActive(false);
			button.SetActive(false);

			transition = GameObject.Find("Transition").GetComponent<Image>();
        }
    }

	public IEnumerator SlowLoadScene(int scene, Image transition)
	{
		for (int i = 0; i < 100; i++)
		{
			transition.color = new Color(1 - i / 100f, 1 - i / 100f, 1 - i / 100f, i / 100f);
			yield return new WaitForSeconds(0.01f);
		}

		SceneManager.LoadScene(scene);
		battleManager.SetState(new CutsceneState());
		yield return null;
	}

	public void ResetGame()
	{
		battleManager.ResetGame();
	}
}