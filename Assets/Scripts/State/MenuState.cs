using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuState : State
{
	public override IEnumerator Start()
	{
		//Go to main menu
		if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
		{
			GameManager.Instance.ExecuteCoroutine(GameManager.Instance.SlowLoadScene(0, GameManager.Instance.transition));
		}

		Debug.Log("MenuState start");
		yield return null;
	}
}