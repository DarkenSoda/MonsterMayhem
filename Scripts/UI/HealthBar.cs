using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image healthBarFadeImage;

    [Header("HealthFade Cooldown")]
    [SerializeField] private float healthFadeCooldown = 1f;

    private void Start() {
        healthBarImage.fillAmount = playerHealth.GetHealthBar();
    }

    private void OnEnable() {
        if (playerHealth != null) playerHealth.OnGettingHit += playerHealth_OnGettingHit;
    }

    private void OnDisable() {
        if (playerHealth != null) playerHealth.OnGettingHit -= playerHealth_OnGettingHit;
    }

    private void playerHealth_OnGettingHit(object sender, EventArgs e) {
        healthBarImage.fillAmount = playerHealth.GetHealthBar() / playerHealth.GetHealthBarMax();
        InvokeRepeating("HealthBarFade", healthFadeCooldown, 0.01f);
    }

    private void HealthBarFade() {
        if (healthBarFadeImage.fillAmount > healthBarImage.fillAmount) {
            healthBarFadeImage.fillAmount -= SystemVariables.speedMultiplier * Time.deltaTime;
        }

        if (healthBarFadeImage.fillAmount <= healthBarImage.fillAmount) {
            CancelInvoke("HealthBarFade");
        }
    }
}
