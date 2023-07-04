using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationSpot : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] MeshRenderer meshRenderer;

    public Transform GetTarget() {
        return target;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}