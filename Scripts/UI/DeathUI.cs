using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour {
    [SerializeField] YouDiedUI youDiedUI;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button newGameButton;
    [SerializeField] Button mainMenuButton;

    private void Start() {
        tryAgainButton.onClick.AddListener(() => {
            Loader.Load(Loader.targetScene);
        });
        newGameButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.FirstLevel);
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
