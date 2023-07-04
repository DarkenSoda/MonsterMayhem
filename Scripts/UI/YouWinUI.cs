using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinUI : MonoBehaviour {
    [SerializeField] Button newGameButton;
    [SerializeField] Button mainMenuButton;
    void Start() {
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
