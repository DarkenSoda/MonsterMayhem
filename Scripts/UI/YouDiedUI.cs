using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedUI : MonoBehaviour {
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private DeathUI deathUI;

    private void Start() {
        playerHealth.OnDeath += OnDeath;
        Hide();
    }

    private void OnDeath(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void DeathTimer() {
        StartCoroutine(OnDeathMenuUI());
    }

    private IEnumerator OnDeathMenuUI() {
        yield return new WaitForSeconds(2f);

        deathUI.Show();
        Time.timeScale = 0f;
    }
}
