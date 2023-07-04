using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private SettingsMenu settingsMenu;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManagerScript.Instance.TogglePauseGame();
        });

        settingsButton.onClick.AddListener(() => {
            settingsMenu.Show();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });
    }

    private void Start() {
        GameManagerScript.Instance.OnGamePaused += OnGamePaused;
        GameManagerScript.Instance.OnGameUnPaused += OnGameUnPaused;

        Hide();
    }

    private void OnGamePaused(object sender, EventArgs e) {
        Show();
    }

    private void OnGameUnPaused(object sender, EventArgs e) {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
