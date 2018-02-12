﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int playerMaxHealth;
    public float playerCurrentHealth;
    [SerializeField] float dmgPerHit = 15f;
    //[SerializeField] float minTimeBetweenHits = 0.5f;
    //[SerializeField] float maxAttackRange = 3f;
    [SerializeField] int enemyLayer = 9;
    int attk = 0;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] float attackCD;
    [SerializeField] float abilityCD = 0f;
    [SerializeField] float ability2CD = 0f;
    bool useability = false;
    float healAmount = 25f;
    Animator animator;

    [SerializeField] WeaponControl weaponInUse;
    [SerializeField] GameObject weaponSocket;
    [SerializeField] AnimatorOverrideController animatorOverrideController;
    ManaManager mana;

    //[SerializeField] SpecialAbility[] abilities;

    float lastHitTime = 0f;

    CameraRaycaster cameraRaycaster;
    GameObject currentTarget;
    public GameObject ability;

    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        mana = GetComponent<ManaManager>();
        //cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        PutWeaponInHand();
        SetupRuntimeAnimator();
        attackCD = weaponInUse.GetMinTimeBetweenHits();
        //abilities[0].AttachComponentTo(gameObject);
    }

    public bool GetAttackingState()
    {
        return isAttacking;
    }

    private void SetupRuntimeAnimator()
    {
        animator = GetComponent<Animator>();
        //animator.runtimeAnimatorController = animatorOverrideController;
        //animatorOverrideController["2Hand-Axe-Attack1"] = weaponInUse.GetAttackAnimClip();
    }

    private void PutWeaponInHand()
    {
        var weaponPrefab = weaponInUse.GetWeaponPrefab();
        var weapon = Instantiate(weaponPrefab, weaponSocket.transform);
        weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
        weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);

            //TODO: RESPAWN
        }

        PlayerAttack();

    }

    private void PlayerAttack()
    {
        //player basic attack
        if (Input.GetMouseButton(0))
        {
            if ((Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits()))
            {
                isAttacking = true;
                attackCD = weaponInUse.GetMinTimeBetweenHits();
                attk += 1;
                animator.SetTrigger("Attacking");
                animator.SetInteger("Attack", attk);

                lastHitTime = Time.time;

            }

            //isAttacking = false;
            if (attk >= 3)
            {
                attk = 0;
            }

        }
        if (isAttacking)
        {
            attackCD -= Time.deltaTime;
            if (attackCD <= 0)
            {
                isAttacking = false;
            }
        }
        AttemptAbility1();
        AttemptAbility2();
    }

    private void AttemptAbility1()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (abilityCD <= 0)
            {
                var manaComponent = GetComponent<ManaManager>();
                var manaCost = 10;
                if (manaComponent.isManaAvailable(manaCost))
                {
                    manaComponent.UseMana(manaCost);
                    StartCoroutine(SpecialAttack());
                    abilityCD = 3f;
                }
            }

        }
        if (abilityCD > 0)
        {
            abilityCD -= Time.deltaTime;
        }
        if (abilityCD <= 0)
        {
            abilityCD = 0;
        }
    }
    private void AttemptAbility2()
    {
        if (playerCurrentHealth != playerMaxHealth) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                if (ability2CD <= 0)
                {
                    var manaComponent = GetComponent<ManaManager>();
                    var manaCost = 5;
                    if (manaComponent.isManaAvailable(manaCost))
                    {
                        manaComponent.UseMana(manaCost);
                        if ((playerCurrentHealth + healAmount) > playerMaxHealth)
                        {
                            playerCurrentHealth = playerMaxHealth;
                        }else
                        {
                            playerCurrentHealth += 30;
                        }
                        ability2CD = 6f;
                    }
                }
            }

        }
        if (ability2CD > 0)
        {
            ability2CD -= Time.deltaTime;
        }
        if (ability2CD <= 0)
        {
            ability2CD = 0;
        }
    }
    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(0.1f);

        Instantiate(ability, transform.position + transform.forward * 2.5f, Quaternion.identity);

    }
    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            var enemy = raycastHit.collider.gameObject;
            if (IsTargetInRange(enemy))
            {
                AttackTarget(enemy);
            }

        }
    }

    private void AttackTarget(GameObject target)
    {
        var enemyComponent = target.GetComponent<Enemy>();
        if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
        {
            animator.SetTrigger("Attacking");
            enemyComponent.HurtEnemy(dmgPerHit);
            lastHitTime = Time.time;
        }
    }

    private bool IsTargetInRange(GameObject target)
    {
        //check if enemy in range
        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        return distanceToTarget <= weaponInUse.GetMaxAttackRange();
    }

    public void HurtPlayer(float damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

}
