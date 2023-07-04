using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    [Header("Stamina")]
    [SerializeField] private float staminaBarMax = 10f;

    [Header("Reveal Enemies")]
    [SerializeField] private float revealEnemiesStaminaCost = 5f;
    [SerializeField] private float revealEnemiesCooldownMax = 3f;
    [SerializeField] private float revealEnemiesRadius = 80f;
    [SerializeField] private LayerMask enemyLayer;
    private List<Collider> enemyInRange;
    private const string ENEMY = "Enemy";
    private const string ENEMYOUTLINE = "EnemyOutline";

    [Header("Teleportation")]
    [SerializeField] private float teleportStaminaCost = 2.5f;
    [SerializeField] private float teleportDistance = 25f;
    [SerializeField] private float teleportCooldownMax = 1.5f;
    [SerializeField] private TeleportationSpot teleportationSpot;
    [SerializeField] LayerMask teleportRayCastLayer;

    [Header("Inputs")]
    [SerializeField] GameInputs gameInput;

    private bool canReveal = false;
    private float revealEnemiesCooldown;
    private bool canTeleport = false;
    private float teleportCooldown;
    private float staminaBar;
    private bool isDead = false;

    private void Start() {
        gameInput.OnTeleportStarted += GameInput_OnTeleportStarted;
        gameInput.OnTeleportFinished += GameInput_OnTeleportFinished;
        GetComponentInChildren<PlayerHealth>().OnDeath += OnPlayerDeath;
        gameInput.OnRevealEnemiesStarted += GameInput_OnRevealEnemiesStarted;
        gameInput.OnRevealEnemiesFinished += GameInput_OnRevealEnemiesFinished;


        revealEnemiesCooldown = revealEnemiesCooldownMax;
        teleportCooldown = teleportCooldownMax;
        staminaBar = staminaBarMax;
        teleportationSpot.Hide();
    }

    private void Update() {
        revealEnemiesCooldown += (SystemVariables.speedMultiplier * Time.deltaTime);
        teleportCooldown += (SystemVariables.speedMultiplier * Time.deltaTime);

        if (staminaBar < staminaBarMax) staminaBar += (SystemVariables.speedMultiplier * Time.deltaTime);
    }

    private void FixedUpdate() {
        HandleTeleportTarget();
    }

    private void GameInput_OnTeleportStarted(object sender, EventArgs e) {
        if (isDead) return;
        if (staminaBar < teleportStaminaCost) return;
        if (teleportCooldown < teleportCooldownMax) return;
        canTeleport = true;
    }

    private void GameInput_OnTeleportFinished(object sender, EventArgs e) {
        if (!canTeleport) return;
        if (staminaBar < teleportStaminaCost) return;
        if (teleportCooldown < teleportCooldownMax) return;

        canTeleport = false;
        teleportCooldown = 0f;
        staminaBar -= teleportStaminaCost;

        GetComponent<CharacterController>().enabled = false;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, teleportDistance, teleportRayCastLayer)) {
            transform.position = hit.point;
        } else {
            transform.position = teleportationSpot.GetTarget().position;
        }
        GetComponent<CharacterController>().enabled = true;
        teleportationSpot.Hide();
    }

    private void HandleTeleportTarget() {
        if (!canTeleport) return;

        teleportationSpot.Show();

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, teleportDistance, teleportRayCastLayer)) {
            teleportationSpot.transform.position = hit.point;
            teleportationSpot.transform.rotation = Quaternion.LookRotation(hit.normal);
        } else {
            teleportationSpot.transform.position = ray.GetPoint(teleportDistance);
        }
    }

    private void GameInput_OnRevealEnemiesStarted(object sender, EventArgs e) {
        if (isDead) return;
        if (staminaBar < revealEnemiesStaminaCost) return;
        if (revealEnemiesCooldown < revealEnemiesCooldownMax) return;
        canReveal = true;
        revealEnemiesCooldown = 0f;

        staminaBar -= revealEnemiesStaminaCost;
        SystemVariables.ChangeSpeedMultiplier(SystemVariables.minSpeedMultiplier);

        // Reveal Enemies logic
        enemyInRange = new List<Collider>(Physics.OverlapSphere(transform.position, revealEnemiesRadius, enemyLayer));
        int enemyOutlineLayer = LayerMask.NameToLayer(ENEMYOUTLINE);
        foreach (var enemy in enemyInRange) {
            SwitchEnemyLayer(enemy, enemyOutlineLayer);
        }
    }

    private void GameInput_OnRevealEnemiesFinished(object sender, EventArgs e) {
        if (!canReveal) return;
        canReveal = false;

        SystemVariables.ChangeSpeedMultiplier(SystemVariables.defaultMultiplier);

        // End Ability
        int enemyLayer = LayerMask.NameToLayer(ENEMY);
        foreach (var enemy in enemyInRange) {
            SwitchEnemyLayer(enemy, enemyLayer);
        }
        enemyInRange.Clear();
    }

    private void SwitchEnemyLayer(Collider enemy, int layer) {
        if (enemy == null) return;

        var children = enemy.GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (var child in children) {
            child.gameObject.layer = layer;
        }
    }

    private void OnPlayerDeath(object sender, EventArgs e) {
        isDead = true;
    }

    public float GetStaminaBar() {
        return staminaBar;
    }

    public float GetStaminaBarMax() {
        return staminaBarMax;
    }
}