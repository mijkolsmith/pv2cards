using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
			List<CanvasRenderer> panels = FindObjectsOfType<CanvasRenderer>().Where(x => x.name == "HandPanel" || x.name == "ArenaPanel").ToList();
			handPanel = panels.Where(x => x.name == "HandPanel").First().gameObject;
			arenaPanel = panels.Where(x => x.name == "ArenaPanel").First().gameObject;
        }
    }

    public void ResetGame()
	{
		battleManager.ResetGame();
	}
}