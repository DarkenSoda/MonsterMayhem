using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderValue : MonoBehaviour {
    [SerializeField] TextMeshProUGUI sliderText;

    private void Start() {
        GetComponent<Slider>().onValueChanged.AddListener(delegate { UpdateText(); });
        sliderText.text = GetComponent<Slider>().value.ToString();
    }

    private void UpdateText() {
        sliderText.text = GetComponent<Slider>().value.ToString();
    }
}
