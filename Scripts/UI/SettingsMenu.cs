using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private Button goBackButton;
    [SerializeField] private Slider horizontalSlider;
    [SerializeField] private Slider verticalSlider;

    private void Start() {
        goBackButton.onClick.AddListener(() => {
            SaveAndReturn();
        });

        horizontalSlider.value = SystemVariables.horizontalSensitivityMultiplier * 5;
        verticalSlider.value = SystemVariables.verticalSensitivityMultiplier * 5;

        Hide();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SaveAndReturn();
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void SaveChanges() {
        SystemVariables.ChangeXSensitivity(horizontalSlider.value);
        SystemVariables.ChangeYSensitivity(verticalSlider.value);
    }

    private void SaveAndReturn() {
        SaveChanges();
        Hide();
    }
}
