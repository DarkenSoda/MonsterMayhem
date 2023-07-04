using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    [SerializeField] private int enemyCount;
    [SerializeField] private YouWinUIText youWinUIText;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public static GameManagerScript Instance;

    private bool isGamePaused = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInputs.Instance.OnPauseClicked += OnPauseClicked;
        EnemyHealth.OnEnemyDeathEvent += OnEnemyDeathEvent;
        
        Debug.Log("Enemy Count: " + enemyCount);
    }

    private void OnEnemyDeathEvent(object sender, EventArgs e) {
        enemyCount--;

        Debug.Log("Enemy Count: " + enemyCount);
        if (enemyCount <= 0) {
            youWinUIText.Show();
        }
    }

    private void OnDestroy() {
        GameInputs.Instance.OnPauseClicked -= OnPauseClicked;
        EnemyHealth.OnEnemyDeathEvent -= OnEnemyDeathEvent;
    }

    private void OnPauseClicked(object sender, EventArgs e) {
        TogglePauseGame();
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;

        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePaused() {
        return isGamePaused;
    }
}
