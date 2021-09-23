using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthValue : MonoBehaviour
{
    public Slider slider;
    TextMeshProUGUI tmp;

    void Start()
    {
         tmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        tmp.text = slider.value.ToString();
    }
}
