using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharController : MonoBehaviour {
    [Header("Movement and Gravity")]
    [SerializeField] protected float moveSpeed = 7f;
    [SerializeField] protected float rotationSpeed = 10f;
    [SerializeField] protected float slidingSpeed = 10f;
    [SerializeField] protected float gravityMultiplier = 5f;

    [Header("Floor and Feet")]
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Transform body;
    [SerializeField] private float checkSphereDistance = .2f;

    protected float velocity;
    private float gravity = -9.81f;
    protected bool canJump = false;
    protected bool isGrounded = false;
    protected bool isSliding;
    protected CharacterController cController;

    private RaycastHit hitInfo;
    private Vector3 slidingDirection;

    private void Start() {
        cController = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        isGrounded = Physics.CheckSphere(feetTransform.position, checkSphereDistance, floorLayer);
        isSliding = OnSlope();
        canJump = isGrounded && !isSliding;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feetTransform.position, checkSphereDistance);
    }

    public void HandleSlopeSliding() {
        if (!isSliding) return;

        slidingDirection = Vector3.ProjectOnPlane(Vector3.down, hitInfo.normal);
        cController.Move(slidingDirection * slidingSpeed * Time.deltaTime * SystemVariables.speedMultiplier);
    }

    private bool OnSlope() {
        if (Physics.SphereCast(body.position, checkSphereDistance, Vector3.down, out hitInfo, 1f, floorLayer)) {
            // Slope Angle
            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);
            print(angle);
            if (angle > cController.slopeLimit) {
                return true;
            }
        }

        return false;
    }

    public void HandleGravity() {
        if (isGrounded && velocity < 0f) {
            velocity = 0f;
        } else {
            velocity += gravity * (SystemVariables.speedMultiplier * Time.deltaTime);
        }

        cController.Move(new Vector3(0f, velocity, 0f) * gravityMultiplier * (SystemVariables.speedMultiplier * Time.deltaTime));
    }

    public Vector3 AdjustVelocityToSlope(Vector3 velocity, Transform transform) {
        // align move vector with the ramp
        Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        Vector3 adjustedVeclocity = slopeRotation * velocity;
        if (adjustedVeclocity.y < 0) return adjustedVeclocity;

        return velocity;
    }
}
