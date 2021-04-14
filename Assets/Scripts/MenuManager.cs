using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject options;

	public void NextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void ToggleOptions()
	{
		options.SetActive(options.activeInHierarchy ? false : true);
	}

	public void Quit()
	{
		Application.Quit();
	}
}