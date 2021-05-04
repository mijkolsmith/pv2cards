using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public Player player;
	public BattleManager battleManager;

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
		player = new Player();
	}

	public void ResetGame()
	{
		battleManager.ResetGame();

		player.health = 10;
	}
}