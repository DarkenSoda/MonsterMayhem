using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour {
    private NavMeshAgent enemyAgent;
    private float agentSpeed;
    private float agentAngularSpeed;
    private float agentAcceleration;

    [Header("Detection Radius")]
    [SerializeField] private float enemyInnerRadius = 3f;
    [SerializeField] private float enemyOuterRadius = 10f;

    [Header("Player Target")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;

    [Header("Partrolling Waypoints")]
    [SerializeField] private float waitCooldownMax = 3f;
    [SerializeField] private float maxStoppingDistance = 3f;
    [SerializeField] private Transform[] wayPointList;
    private int currentWayPoint;
    private float waitCooldown = 0f;

    private bool isDead = false;
    private bool isWalking = false;

    private bool playerInSightRange;
    private bool playerInAttackRange;
    private EnemyState state;

    private void Start() {
        GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath;
        enemyAgent = GetComponent<NavMeshAgent>();
        agentSpeed = enemyAgent.speed;
        agentAngularSpeed = enemyAgent.angularSpeed;
        agentAcceleration = enemyAgent.acceleration;
    }

    private void Update() {
        if (isDead) return;

        enemyAgent.speed = agentSpeed * SystemVariables.speedMultiplier;
        enemyAgent.angularSpeed = agentAngularSpeed * SystemVariables.speedMultiplier;
        enemyAgent.acceleration = agentAcceleration * SystemVariables.speedMultiplier;
        playerInAttackRange = Physics.CheckSphere(transform.position + Vector3.up, enemyInnerRadius, playerLayer);
        playerInSightRange = Physics.CheckSphere(transform.position + Vector3.up, enemyOuterRadius, playerLayer);

        if (!GetComponent<EnemyAnimator>().IsAttacking()) {
            if (playerInAttackRange) state = EnemyState.Attack;
            else if (playerInSightRange) state = EnemyState.Chase;
            else state = EnemyState.Idle;
        }

        waitCooldown += Time.deltaTime;
    }

    private void FixedUpdate() {
        if (isDead) return;

        switch (state) {
            case EnemyState.Idle:
                enemyAgent.enabled = true;
                enemyAgent.stoppingDistance = 0f;
                PatrolArea();
                break;
            case EnemyState.Chase:
                enemyAgent.enabled = true;
                enemyAgent.stoppingDistance = maxStoppingDistance;
                MoveEnemyToTarget(player);
                break;
            case EnemyState.Attack:
                // if(!isAttacking) Rotate Enemy to Face Player

                isWalking = false;
                enemyAgent.enabled = false;
                break;
        }
    }

    private void PatrolArea() {
        if (waitCooldown < waitCooldownMax) return;

        if (currentWayPoint >= wayPointList.Length) {
            currentWayPoint = 0;
        }

        MoveEnemyToTarget(wayPointList[currentWayPoint]);

        if (WayPointReached()) {
            isWalking = false;
            currentWayPoint++;
            waitCooldown = 0f;
        }
    }

    private bool WayPointReached() {
        Transform wayPoint = wayPointList[currentWayPoint];
        wayPoint.position = new Vector3(wayPoint.position.x, transform.position.y, wayPoint.position.z);

        if (Vector3.Distance(transform.position, wayPoint.position) <= 0.5f)
            return true;

        return false;
    }

    private void MoveEnemyToTarget(Transform target) {
        isWalking = true;
        enemyAgent.SetDestination(target.position);
    }

    private void OnEnemyDeath(object sender, EventArgs e) {
        isDead = true;
    }

    public bool IsWalking() {
        return isWalking;
    }

    public EnemyState GetEnemyState() {
        return state;
    }
}