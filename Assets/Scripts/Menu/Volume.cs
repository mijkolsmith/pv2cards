using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
	Slider slider;
	public AudioMixer mixer;
	private void Awake()
	{
		slider = GetComponent<Slider>();
	}

    private void Start()
    {
		slider.value = PlayerPrefs.GetFloat("Volume", 0.6f);
		mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .1f) * 45 + 15);

	}

	public void SetValue()
	{
		if (slider.value != 0)
		{
			mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .1f) * 45 + 15);
		}
		else
		{
			mixer.SetFloat("MasterVol", -60);
		}
		PlayerPrefs.SetFloat("Volume", slider.value);
	}
}
