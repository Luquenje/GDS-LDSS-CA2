using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] float secondsBetweenShots = 5f;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);
    [SerializeField] AStarSteeringBehaviour aStar;
    [SerializeField] bool patrolling = false;
    Animator animator;
    public Transform waypoint;//Denote the start and end object in the scene

    public AudioSource MeleeSwingSFX;
    public AudioSource fireBallSFX;

    public bool weaponSwing = false;
    [SerializeField] float pathfindCD = 0.5f;
    [SerializeField] float currentHealthPoints;
    [SerializeField]bool isAttacking = false;
    [SerializeField] bool attack = false;
    bool powerAttacking = false;
    [SerializeField] float attackCD = 5f;
    [SerializeField] float skillCD = 5f;
    Pathfinder pathFinder = null;
    GameObject player = null;
    bool pathfindBool = false;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathFinder = GetComponent<Pathfinder>();
        aStar = GetComponent<AStarSteeringBehaviour>();
        animator = GetComponent<Animator>();
        currentHealthPoints = maxHealthPoints;

    }
    void Update()
    {
        if(currentHealthPoints <= 0)
        {
            aStar.currentState = AStarSteeringBehaviour.AIState.IDLE;
            animator.SetBool("Dead", true);
        }

        float distanceToPlyaer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlyaer <= attackRadius /*&& !isAttacking*/ && !attack)
        {
            //aiControl.SetTarget(player.transform);
            isAttacking = true;
            attack = true;
            aStar.currentState = AStarSteeringBehaviour.AIState.IDLE;
            //InvokeRepeating("Shoot", 0f, secondsBetweenShots);
            Attack();
            weaponSwing = false;
            CancelInvoke();
            powerAttacking = false;
        }

        //attack cooldown
        if (attack)
        {
            attackCD -= Time.deltaTime;
            if (attackCD <= 0)
            {
                attack = false;
                attackCD = 5f;
            }
        }
        //if (powerAttacking)
        //{
        //    skillCD -= Time.deltaTime;
        //    if (skillCD <= 0)
        //    {
        //        powerAttacking = false;
        //        skillCD = 5f;
        //    }
        //}

        if (distanceToPlyaer > attackRadius)
        {
            isAttacking = false;
            
            if (patrolling)
            {
                aStar.currentState = AStarSteeringBehaviour.AIState.WAYPOINTS;
            }
            //if (!patrolling)
            //{
            //    aStar.currentState = AStarSteeringBehaviour.AIState.IDLE;
            //}

            //pathFinder.FindPath();
        }
        if ((distanceToPlyaer <= moveRadius) && (distanceToPlyaer > attackRadius))
        {
            //aiControl.SetTarget(player.transform);
            if (!powerAttacking)
            {
                powerAttacking = true;
                InvokeRepeating("Shoot", 0f, secondsBetweenShots);
                //Shoot();
            }
            if (currentHealthPoints <= 100)
            {
                FindPathToPlayer();
            }
        }
        if (distanceToPlyaer > moveRadius)
        {
            //aiControl.SetTarget(transform);
            pathFinder.end = waypoint;
            //CancelInvoke();
            if (pathfindBool)
            {
                pathFinder.FindPath();
                pathfindBool = false;
            }
            CancelInvoke();
            powerAttacking = false;
        }
    }

    private void FindPathToPlayer()
    {
        pathFinder.end = player.transform;
        //aStar.currentState = AStarSteeringBehaviour.AIState.WAYPOINTS;
        //InvokeRepeating("PathToPlayer", 0f, 5f);

        if (!pathfindBool)
        {
            pathFinder.FindPath();
            pathfindBool = true;
        }
        pathfindCD -= Time.deltaTime;
        if (pathfindCD <= 0)
        {
            pathfindBool = false;
            pathfindCD = 0.5f;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attacking");
        weaponSwing = true;
        MeleeSwingSFX.Play();
    }
    public bool Attacking()
    {
        return isAttacking;
    }
    void PathToPlayer()
    {
        pathFinder.FindPath();
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - bulletSpawn.transform.position).normalized;
        newBullet.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        animator.SetTrigger("Skill");
        fireBallSFX.Play();
    }

    public void HurtEnemy(float damageToGive)
    {
        currentHealthPoints -= damageToGive;
    }

    public void SetMaxHealth()
    {
        currentHealthPoints = maxHealthPoints;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 255f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, moveRadius);
        Gizmos.color = new Color(255f, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}

