using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeColliderHandler : MonoBehaviour {
    public event EventHandler<Collider> OnAxeTrigger;

    private void OnTriggerEnter(Collider other) {
        OnAxeTrigger?.Invoke(this, other);
    }
}
