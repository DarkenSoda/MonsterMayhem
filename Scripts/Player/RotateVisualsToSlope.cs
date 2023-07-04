using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVisualsToSlope : MonoBehaviour {

    private void FixedUpdate() {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f)) {
            // align Visuals with the ramp
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
        }
    }
}
