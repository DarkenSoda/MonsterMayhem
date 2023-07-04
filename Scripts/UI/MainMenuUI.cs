using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SettingsMenu settingsMenu;

    private void Start() {
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.FirstLevel);
        });

        settingsButton.onClick.AddListener(() => {
            settingsMenu.Show();
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
