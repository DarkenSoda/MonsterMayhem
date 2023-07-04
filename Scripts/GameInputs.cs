using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour {

    public event EventHandler OnJumpPerformed;
    public event EventHandler OnAttackPerformed;
    public event EventHandler OnRevealEnemiesStarted;
    public event EventHandler OnRevealEnemiesFinished;
    public event EventHandler OnTeleportStarted;
    public event EventHandler OnTeleportFinished;
    public event EventHandler OnPauseClicked;

    public static GameInputs Instance { get; private set; }

    PlayerInputs playerInputs;

    private void Awake() {
        Instance = this;
        playerInputs = new PlayerInputs();
    }

    private void Start() {
        playerInputs.Player.Enable();
        playerInputs.Player.Jump.performed += OnJump_Performed;
        playerInputs.Player.Attack.performed += OnAttack_Performed;
        playerInputs.Player.RevealEnemies.started += OnRevealEnemies_Started;
        playerInputs.Player.RevealEnemies.performed += OnRevealEnemies_Finished;
        playerInputs.Player.RevealEnemies.canceled += OnRevealEnemies_Finished;
        playerInputs.Player.Teleport.started += OnTeleport_Started;
        playerInputs.Player.Teleport.canceled += OnTeleport_Finished;
        playerInputs.Player.Pause.performed += OnPausePerformed;
    }

    private void OnDestroy() {
        playerInputs.Player.Jump.performed -= OnJump_Performed;
        playerInputs.Player.Attack.performed -= OnAttack_Performed;
        playerInputs.Player.RevealEnemies.started -= OnRevealEnemies_Started;
        playerInputs.Player.RevealEnemies.performed -= OnRevealEnemies_Finished;
        playerInputs.Player.RevealEnemies.canceled -= OnRevealEnemies_Finished;
        playerInputs.Player.Teleport.started -= OnTeleport_Started;
        playerInputs.Player.Teleport.canceled -= OnTeleport_Finished;
        playerInputs.Player.Pause.performed -= OnPausePerformed;

        playerInputs.Dispose();
    }

    private void OnTeleport_Started(InputAction.CallbackContext obj) {
        OnTeleportStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnTeleport_Finished(InputAction.CallbackContext obj) {
        OnTeleportFinished?.Invoke(this, EventArgs.Empty);
    }

    private void OnRevealEnemies_Started(InputAction.CallbackContext obj) {
        OnRevealEnemiesStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnRevealEnemies_Finished(InputAction.CallbackContext obj) {
        OnRevealEnemiesFinished?.Invoke(this, EventArgs.Empty);
    }

    private void OnJump_Performed(InputAction.CallbackContext obj) {
        OnJumpPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void OnAttack_Performed(InputAction.CallbackContext obj) {
        if (GameManagerScript.Instance.IsGamePaused()) return;
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void OnPausePerformed(InputAction.CallbackContext obj) {
        OnPauseClicked?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputs.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}