using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    [SerializeField] float dmgPerHit = 15f;
    //[SerializeField] float minTimeBetweenHits = 0.5f;
    //[SerializeField] float maxAttackRange = 3f;
    [SerializeField] int enemyLayer = 9;
    int attk = 0;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] float attackCD;

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
                animator.SetInteger("Attack", attk);//1 is random no.

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
        if (Input.GetMouseButtonDown(1))
        {
            AttemptAbility(0);
        }
    }

    private void AttemptAbility(int abilityIndex)
    {
        var manaComponent = GetComponent<ManaManager>();
        var manaCost = 10;
        if (manaComponent.isManaAvailable(manaCost))
        {
            manaComponent.UseMana(manaCost);
            StartCoroutine(SpecialAttack());
            //var abilityParams = new AbilityUseParams(dmgPerHit);
            //abilities[abilityIndex].Use(abilityParams);
            //use ability

        }

        //mana.UseMana(10);
    }
    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(0.4f);

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

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

}
