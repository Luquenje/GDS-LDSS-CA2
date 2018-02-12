using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float moveRadius = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] float secondsBetweenShots = 1.3f;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);
    [SerializeField] AStarSteeringBehaviour aStar;
    [SerializeField] bool patrolling = false;
    public Animator animator;
    public Transform waypoint;//Denote the start and end object in the scene

    public bool weaponSwing = false;
    [SerializeField] float pathfindCD = 0.5f;
    [SerializeField] float currentHealthPoints;
    [SerializeField]bool isAttacking = false;
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
        if (distanceToPlyaer <= attackRadius && !isAttacking)
        {
            //aiControl.SetTarget(player.transform);
            isAttacking = true;
            aStar.currentState = AStarSteeringBehaviour.AIState.IDLE;
            InvokeRepeating("Attack", 0f, secondsBetweenShots);
            weaponSwing = false;
        }
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
            CancelInvoke();
            //pathFinder.FindPath();
        }
        //else
        //{
        //    aiControl.SetTarget(transform);
        //}
        if ((distanceToPlyaer <= moveRadius) && (distanceToPlyaer > attackRadius))
        {
            //aiControl.SetTarget(player.transform);
            pathFinder.end = player.transform;
            //aStar.currentState = AStarSteeringBehaviour.AIState.WAYPOINTS;
            //InvokeRepeating("PathToPlayer", 0f, 5f);
            
            if(!pathfindBool)
            {
                pathFinder.FindPath();
                pathfindBool = true;
            }
            pathfindCD -= Time.deltaTime;
            if(pathfindCD <= 0)
            {
                pathfindBool = false;
                pathfindCD = 0.5f;
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
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attacking");
        weaponSwing = true;
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

