using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharController {

    public static PlayerMovement Instance { get; private set; }

    [Header("Jump Force")]
    [SerializeField] private float jumpForce = 7f;

    [Header("Inputs")]
    [SerializeField] GameInputs gameInput;

    private bool isWalking;
    private bool isDead = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        cController = GetComponent<CharacterController>();
        gameInput.OnJumpPerformed += GameInput_OnJumpPerformed;
        GetComponentInChildren<PlayerHealth>().OnDeath += OnPlayerDeath;
    }

    private void Update() {
        HandleMovement();
        HandleGravity();
        HandleSlopeSliding();
    }

    private void HandleMovement() {
        if (isDead) return;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        float moveDistance = moveSpeed * SystemVariables.speedMultiplier * Time.deltaTime;
        if (inputVector != Vector2.zero) {
            // calculate rotation angle from input vector
            float targetRotation = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            // Rotate character
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, 0.1f);
            Vector3 moveDir = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

            moveDir = AdjustVelocityToSlope(moveDir, transform);

            if (!GetComponent<PlayerAnimations>().IsAttacking()) {
                // move character in direction
                cController.Move(moveDir * moveDistance);
            } else {
                isWalking = false;
                return;
            }
        }

        isWalking = inputVector != Vector2.zero;
    }

    private void GameInput_OnJumpPerformed(object sender, EventArgs e) {
        if (isDead) return;
        if (!isGrounded) return;
        if (!canJump) return;
        if (GetComponent<PlayerAnimations>().IsAttacking()) return;

        velocity += jumpForce;
    }

    private void OnPlayerDeath(object sender, EventArgs e) {
        isDead = true;
    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsJumping() {
        return !isGrounded;
    }
}