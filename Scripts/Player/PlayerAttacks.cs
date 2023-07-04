using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {
    [SerializeField] private BoxCollider axeCollider;
    private float damage;

    private void Start() {
        GetComponentInChildren<AxeColliderHandler>().OnAxeTrigger += OnAxeHitTrigger;
    }

    public void triggerOn() {
        axeCollider.enabled = true;
    }

    public void triggerOff() {
        axeCollider.enabled = false;
    }

    private void OnAxeHitTrigger(object sender, Collider collider) {
        if (collider.gameObject.GetComponent<PlayerAttacks>() != null) return;

        if (collider.gameObject.GetComponent<IDamageable>() != null) {
            collider.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
