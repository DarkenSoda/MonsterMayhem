using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {
    public static event EventHandler OnEnemyDeathEvent;
    public event EventHandler OnGettingHit;
    public event EventHandler OnDeath;
    [SerializeField] private float healthBarMax;
    private float healthBar;

    [Header("Destroy Object Cooldown")]
    [SerializeField] private float destroyObjectCooldown = 10f;

    private void Start() {
        healthBar = healthBarMax;
    }

    // public void Heal(float health) {
    //     healthBar += health;
    //     healthBar = healthBar > healthBarMax ? healthBarMax : health;
    // }

    private IEnumerator EnemyDeath() {
        GetComponent<Collider>().enabled = false;
        GetComponentInParent<EnemyAttacks>().triggerOff(0);
        GetComponentInParent<EnemyAttacks>().triggerOff(1);

        yield return new WaitForSeconds(destroyObjectCooldown);

        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage) {
        healthBar -= damage;

        OnGettingHit?.Invoke(this, EventArgs.Empty);

        if (healthBar <= 0f) {
            OnDeath?.Invoke(this, EventArgs.Empty);
            OnEnemyDeathEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(EnemyDeath());
        }

        Debug.Log($"{-damage} damage Taken\nCurrent health: {healthBar}");
    }
}
