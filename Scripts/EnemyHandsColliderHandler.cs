using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandsColliderHandler : MonoBehaviour
{
    public event EventHandler<Collider> OnEnemyAttackHitTrigger;

    private void OnTriggerEnter(Collider other) {
        OnEnemyAttackHitTrigger?.Invoke(this, other);
    }
}
