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
    [SerializeField] float secondsBetweenShots = 1f;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);
    [SerializeField] AStarSteeringBehaviour aStar;


    [SerializeField] float pathfindCD = 0.5f;
    float currentHealthPoints;
    bool isAttacking = false;
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
        currentHealthPoints = maxHealthPoints;

    }
    void Update()
    {
        float distanceToPlyaer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlyaer <= attackRadius && !isAttacking)
        {
            //aiControl.SetTarget(player.transform);
            isAttacking = true;
            aStar.currentState = AStarSteeringBehaviour.AIState.IDLE;
            InvokeRepeating("Shoot", 0f, secondsBetweenShots);
        }
        if (distanceToPlyaer > attackRadius)
        {
            isAttacking = false;
            aStar.currentState = AStarSteeringBehaviour.AIState.WAYPOINTS;
            CancelInvoke();
        }
        //else
        //{
        //    aiControl.SetTarget(transform);
        //}
        if (distanceToPlyaer <= moveRadius)
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
        else
        {
            //aiControl.SetTarget(transform);
            pathFinder.end = GameObject.FindGameObjectWithTag("Waypoint").transform;
            //CancelInvoke();
            if (pathfindBool)
            {
                pathFinder.FindPath();
                pathfindBool = false;
            }
        }
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

