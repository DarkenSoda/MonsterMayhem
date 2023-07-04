using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable {
    public event EventHandler OnGettingHit;
    public event EventHandler OnDeath;

    [SerializeField] private float healthBarMax;
    private float healthBar;

    private void Awake() {
        healthBar = healthBarMax;
    }

    public void Heal(float health) {
        healthBar += health;
        healthBar = healthBar > healthBarMax ? healthBarMax : health;
    }

    private void PlayerDeath() {
        OnDeath?.Invoke(this, EventArgs.Empty);
        GetComponent<Collider>().enabled = false;
    }

    public void TakeDamage(float damage) {
        healthBar -= damage;

        OnGettingHit?.Invoke(this, EventArgs.Empty);

        if (healthBar <= 0f) {
            PlayerDeath();
        }
    }

    public float GetHealthBar() {
        return healthBar;
    }

    public float GetHealthBarMax() {
        return healthBarMax;
    }
}
