using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {
    private Animator anim;
    private EnemyState state;
    private EnemyAttacks enemyAttacks;

    [SerializeField] private float attackCoolDownTime = .5f;
    private float nextFireTime = 0f;

    private const string HIT = "Hit";
    private const string DEATH = "Death";
    private const string ISWALKING = "isWalking";

    private bool isDead = false;
    private bool isAttacking = false;

    [Header("Attacks Name and Damage")]
    [SerializeField] private List<EnemyAttackAnimation> enemyAttackList = new List<EnemyAttackAnimation>();
    private int enemyAttacksCount = 0;

    private void Start() {
        anim = GetComponent<Animator>();
        enemyAttacks = GetComponent<EnemyAttacks>();
        GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath;
        GetComponent<EnemyHealth>().OnGettingHit += OnEnemyGettingHit;
    }

    private void Update() {
        anim.speed = SystemVariables.speedMultiplier;

        if (isDead) return;

        state = GetComponent<EnemyFollow>().GetEnemyState();
        nextFireTime += Time.deltaTime;

        switch (state) {
            case EnemyState.Idle:
                anim.SetBool(ISWALKING, GetComponent<EnemyFollow>().IsWalking());
                break;
            case EnemyState.Chase:
                anim.SetBool(ISWALKING, true);
                break;
            case EnemyState.Attack:
                anim.SetBool(ISWALKING, false);
                Attack();
                break;
        }
    }

    private void Attack() {
        if (isAttacking || nextFireTime < attackCoolDownTime)
            return;

        isAttacking = true;
        enemyAttacksCount = UnityEngine.Random.Range(0, enemyAttackList.Count);

        anim.SetBool(enemyAttackList[enemyAttacksCount].Name, true);
        enemyAttacks.SetDamage(enemyAttackList[enemyAttacksCount].AttackDamage);
    }

    public void EndAttack() {
        anim.SetBool(enemyAttackList[enemyAttacksCount].Name, false);
        isAttacking = false;
        nextFireTime = 0f;
    }

    private void OnEnemyGettingHit(object sender, EventArgs e) {
        anim.SetTrigger(HIT);
    }

    private void OnEnemyDeath(object sender, EventArgs e) {
        anim.SetTrigger(DEATH);
        isDead = true;
    }

    public bool IsAttacking() {
        return isAttacking;
    }
}