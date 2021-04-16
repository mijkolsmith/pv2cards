using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
	Slider slider;
	private void Awake()
	{
		slider = GetComponent<Slider>();
		if (PlayerPrefs.HasKey("Volume"))
		{
			slider.value = PlayerPrefs.GetFloat("Volume");
		}
	}

	public void SetValue()
	{
		PlayerPrefs.SetFloat("Volume", slider.value);
		GameManager.Instance.volume = slider.value;
	}
}
