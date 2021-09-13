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
		mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .05f) * 30 + 5);

	}

	public void SetValue()
	{
		mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .05f) * 30 + 5);
		PlayerPrefs.SetFloat("Volume", slider.value);
	}
}
