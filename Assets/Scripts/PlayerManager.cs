using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentLv;
    public int currentExp;
    public int[] toLvUp;

    public int[] HPLv;
    public float[] AttLv;
    public int[] ManaLv;

    public int playerCurrentHealth;
    public float currentAtt;

    public AudioSource axeSwingSFX;
    public AudioSource flameTornadoSFX;

    public int playerMaxHealth;
    public GameObject gameoverUI;

    //[SerializeField] float currentAtt = 15f;
    //[SerializeField] float minTimeBetweenHits = 0.5f;
    //[SerializeField] float maxAttackRange = 3f;
    [SerializeField] int enemyLayer = 9;
    int attk = 0;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] float attackCD;
    [SerializeField] public float abilityCD = 0f;
    [SerializeField] public float ability2CD = 0f;
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
    public GameObject ability1;
    public GameObject ability2;

    public AudioSource levelUp;
    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = HPLv[1];
        currentAtt = AttLv[1];
        mana = GetComponent<ManaManager>();
        mana.playerCurrentMana = ManaLv[1];

        cameraRaycaster = FindObjectOfType<CameraRaycaster>();

        //cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        PutWeaponInHand();
        SetupRuntimeAnimator();
        attackCD = weaponInUse.GetMinTimeBetweenHits();
        //gameoverUI = GameObject.FindGameObjectWithTag("DeadMenu");
        //abilities[0].AttachComponentTo(gameObject);
    }
    public float GetCurrentAttack()
    {
        return currentAtt;
    }
    public bool GetAttackingState()
    {
        return isAttacking;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        playerCurrentHealth = currentHealth;
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
        if (currentExp >= toLvUp[currentLv])
        {
            //currentLv++;

            LvUp();
        }



        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            gameoverUI.SetActive(true);
            //TODO: RESPAWN
        }

        PlayerAttack();
        if(playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
        }
    }

    public void LvUp()
    {


        if (currentLv + 1 < 10)
        {
            currentLv++;
        }
        playerCurrentHealth = HPLv[currentLv];
        playerMaxHealth = playerCurrentHealth;


        currentAtt = AttLv[currentLv];
        mana.playerCurrentMana = ManaLv[currentLv];
        mana.playerMaxMana = ManaLv[currentLv];

        levelUp.Play();
    }

    public void AddExp(int experienceToAdd)
    {
        currentExp += experienceToAdd;
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
                axeSwingSFX.Play();
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (abilityCD <= 0)
            {
                var manaComponent = GetComponent<ManaManager>();
                var manaCost = 10;
                if (manaComponent.isManaAvailable(manaCost))
                {
                    flameTornadoSFX.Play();
                    animator.SetTrigger("Skill1");
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
        if (playerCurrentHealth != playerMaxHealth)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                if (ability2CD <= 0)
                {
                    var manaComponent = GetComponent<ManaManager>();
                    var manaCost = 5;
                    if (manaComponent.isManaAvailable(manaCost))
                    {
                        animator.SetTrigger("Heal");
                        manaComponent.UseMana(manaCost);
                        Instantiate(ability2, transform.position, Quaternion.identity, gameObject.transform);
                        if ((playerCurrentHealth + healAmount) > playerMaxHealth)
                        {
                            playerCurrentHealth = playerMaxHealth;
                        }
                        else
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

        Instantiate(ability1, transform.position + transform.forward * 2.5f, Quaternion.identity);
        //flameTornadoSFX.Play();
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
            enemyComponent.HurtEnemy(currentAtt);
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
