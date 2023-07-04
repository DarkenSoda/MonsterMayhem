using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    [SerializeField] private GameInputs gameInputs;
    private PlayerAttacks playerAttacks;

    private Animator anim;
    private const string ISWALKING = "IsWalking";
    private const string JUMPING = "isJumping";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string ATTACK3 = "Attack3";
    private const string HIT = "Hit";
    private const string DEATH = "Death";
    [SerializeField] private float attackCoolDownTime = .5f;
    private float nextFireTime = 0f;

    private bool isAttack1 = false;
    private bool isAttack2 = false;
    private bool isAttack3 = false;
    private bool isAttacking = false;
    private bool isDead = false;

    [SerializeField] private float attackDamage1 = 5f;
    [SerializeField] private float attackDamage2 = 10f;
    [SerializeField] private float attackDamage3 = 15f;

    private void Start() {
        anim = GetComponentInParent<Animator>();
        playerAttacks = GetComponent<PlayerAttacks>();
        gameInputs.OnAttackPerformed += GameInputs_OnAttackPerformed;
        GetComponentInChildren<PlayerHealth>().OnDeath += OnPlayerDeath;
        GetComponentInChildren<PlayerHealth>().OnGettingHit += OnGettingHit;
    }

    private void Update() {
        anim.speed = SystemVariables.speedMultiplier;

        if (isDead) return;

        if (PlayerMovement.Instance.IsWalking()) {
            anim.SetBool(ISWALKING, true);
        } else {
            anim.SetBool(ISWALKING, false);
        }

        if (PlayerMovement.Instance.IsJumping()) {
            anim.SetBool(JUMPING, true);
        } else {
            anim.SetBool(JUMPING, false);
        }

        nextFireTime += (SystemVariables.speedMultiplier * Time.deltaTime);
        isAttack1 = anim.GetBool(ATTACK1);
        isAttack2 = anim.GetBool(ATTACK2);
        isAttack3 = anim.GetBool(ATTACK3);
        isAttacking = isAttack1 || isAttack2 || isAttack3;
    }

    private void OnGettingHit(object sender, EventArgs e) {
        anim.SetTrigger(HIT);
    }

    private void OnPlayerDeath(object sender, EventArgs e) {
        anim.SetTrigger(DEATH);
        isDead = true;
    }

    private void GameInputs_OnAttackPerformed(object sender, EventArgs e) {
        if (isDead) return;
        if (!isAttacking && nextFireTime < attackCoolDownTime)
            return;

        OnClick();
    }

    private void OnClick() {
        if (!isAttacking && !PlayerMovement.Instance.IsJumping()) {
            anim.SetBool(ATTACK1, true);
            isAttacking = true;
            playerAttacks.SetDamage(attackDamage1);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f
        && anim.GetCurrentAnimatorStateInfo(0).IsName(ATTACK1) && isAttack1) {
            anim.SetBool(ATTACK1, false);
            anim.SetBool(ATTACK2, true);
            playerAttacks.SetDamage(attackDamage2);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f
            && anim.GetCurrentAnimatorStateInfo(0).IsName(ATTACK2) && isAttack2) {
            anim.SetBool(ATTACK2, false);
            anim.SetBool(ATTACK3, true);
            playerAttacks.SetDamage(attackDamage3);
        }
    }

    public void EndAttack1() {
        anim.SetBool(ATTACK1, false);
        nextFireTime = 0f;
    }

    public void EndAttack2() {
        anim.SetBool(ATTACK2, false);
        nextFireTime = 0f;
    }

    public void EndAttack3() {
        anim.SetBool(ATTACK3, false);
        nextFireTime = 0f;
    }

    public bool IsAttacking() {
        return isAttacking;
    }
}