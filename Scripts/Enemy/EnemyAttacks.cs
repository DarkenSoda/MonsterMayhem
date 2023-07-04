using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour {
    [SerializeField] private BoxCollider leftHand;
    [SerializeField] private BoxCollider rightHand;
    private float damage;

    private void Start() {
        foreach(var hand in GetComponentsInChildren<EnemyHandsColliderHandler>()) {
            hand.OnEnemyAttackHitTrigger += OnPlayerHitTrigger;
        }
    }

    public void triggerOn(int left) {
        if (left == 0) {
            leftHand.enabled = true;
        } else {
            rightHand.enabled = true;
        }
    }

    public void triggerOff(int left) {
        if (left == 0) {
            leftHand.enabled = false;
        } else {
            rightHand.enabled = false;
        }
    }

    private void OnPlayerHitTrigger(object sender, Collider collider) {
        if (collider.gameObject.GetComponent<EnemyAttacks>() != null) return;

        if (collider.gameObject.GetComponent<IDamageable>() != null) {
            collider.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
