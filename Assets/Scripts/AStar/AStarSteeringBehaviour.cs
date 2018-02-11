using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarSteeringBehaviour : MonoBehaviour {
    public Transform target; //This is the object to seek flee pursuit..etc
    public Transform wanderTarget;//Wandering Target
    public Rigidbody m_RigidBody;//my rigid body
    public Transform turret; //This is the turret object
    public Rigidbody m_Shell;           //Prefab of the shell
    public Transform m_FireTransform;   //Firing Point
    EnemyMovementControl m_Character = null;
    Animator m_Animator;
    public float moveSpeed = 6.0f;
    public float maxSpeed = 6.0f;
    public float rotationSpeed = 1.0f;
    //public float arrivingRange = 5f;
    //Firing rate controller
    public float shootTimer = 0;
    public float shootCooldown = 2; //2 secs
    public enum AIState
    {
        IDLE, SEEK, FLEE, ARRIVE, PURSUIT,
        EVADE, WAYPOINTS, WANDERING, ATTACK
    };
    public AIState currentState;
    public enum WayPointState { ONCE, LOOP, PING_PONG };
    public WayPointState currWayPtState = WayPointState.ONCE;
    public ArrayList waypoints;//Waypoints to visit
    public int currentWaypoint = 0;
    private bool to = true;
    //Variables for avoiddance
    public float sightDistance = 5f;
    public float peripheralDist = 2.5f;
    public float avoidanceAngle = 45; //45 degrees
    public enum AvoidState { SINGLE_RAY, DOUBLE_RAY, TRIPLE_RAY };
    public AvoidState avoidanceState = AvoidState.SINGLE_RAY;
    float animSpeed = 0.8f;
    // Use this for initialization
    void Start()
    {
        m_Character = GetComponent<EnemyMovementControl>();
        m_Animator = GetComponent<Animator>();
        //Get the rigidbody 
        m_RigidBody = GetComponent<Rigidbody>();
        //Get the player's transform as target
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        //if enemy is within range of attack

        //else continue state
        switch (currentState)
        {
            case AIState.IDLE:
                //Perform this when idling
                moveSpeed = 0f;
                animSpeed = 0f;
                break;
            case AIState.SEEK:
                Seek();
                break;
            case AIState.FLEE:
                Flee();
                break;
            //case AIState.ARRIVE:
            //    Arrive();
                break;
            case AIState.PURSUIT:
                break;
            //case AIState.EVADE:
            //    break;
            case AIState.WAYPOINTS:
                animSpeed = 0.8f;
                WayPointNavigation();
                break;
            
        }
        Move();
        //TurnTurret();
        //Update the shootTimer
        //shootTimer += Time.deltaTime;
    }
    void Wander()
    {
        target = wanderTarget;
        Seek();
        //Get the direction:AB=OA-OB (target position - self position)
        Vector3 direction = wanderTarget.position - transform.position;
        //Deterimine the slowing down process if close by
        //direction.magnitude is the dist betw waypoint and NPC
        if (direction.magnitude < 3)
        {
            RaycastHit hitInfo;
            bool hit;
            do
            {
                //Generate a random point based on angle and distance
                float angle = Random.Range(-180, 180);
                float distance = Random.Range(20, 100);
                float z = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
                float x = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
                wanderTarget.position = transform.position + new Vector3(x, 0, z);
                Ray ray = new Ray(
                    wanderTarget.position + wanderTarget.up * 100,
                    -wanderTarget.up);
                hit = Physics.Raycast(ray, out hitInfo);

            } while (!hit || !hitInfo.collider.CompareTag("Terrain"));
        }
    }
    void WayPointNavigation()
    {
        //GameObject wp = GameObject.FindGameObjectWithTag("WayPointGroup1");
        //waypoints = wp.GetComponentsInChildren<Transform>();
        target.position = ((Node) waypoints[currentWaypoint]).position;
        Seek();
        //Get the direction:AB=OA-OB (target position - self position)
        Vector3 direction = target.position - transform.position;
        this.target.position = target.position;
        //Deterimine the slowing down process if close by
        //direction.magnitude is the dist betw waypoint and NPC
        if (direction.magnitude < 3)
        {
            switch (currWayPtState)
            {
                case WayPointState.ONCE:
                    if (++currentWaypoint >= waypoints.Count)
                        currentState = AIState.IDLE;
                    break;
                case WayPointState.LOOP:
                    currentWaypoint++;//Move to the next point
                    currentWaypoint %= waypoints.Count;//This is for loop
                    break;
                case WayPointState.PING_PONG:
                    if (to)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        currentWaypoint--;
                    }
                    //if NPC reaches end of waypoints
                    if (currentWaypoint > waypoints.Count - 1)
                    {
                        to = !to; //reverse
                        currentWaypoint = waypoints.Count - 1;
                    }
                    else if (currentWaypoint < 0)
                    {
                        to = !to;
                        currentWaypoint = 0;
                    }
                    break;
            }

        }
    }
    void Seek()
    {
        //Get the direction:AB=OA-OB (target position - self position)
        Vector3 direction = target.position - transform.position;
        direction.Normalize();//Make it unit vector of 1
        Turn(direction);
    }
    void Flee()
    {
        //Get the direction:AB=OA-OB (target position - self position)
        Vector3 direction = -target.position + transform.position;
        direction.Normalize();//Make it unit vector of 1
        Turn(direction);
    }
    //void Arrive()
    //{
    //    //Get the direction:AB=OA-OB (target position - self position)
    //    Vector3 direction = target.position - transform.position;
    //    //Deterimine the slowing down process if close by
    //    //direction.magnitude is the dist betw enemy and player
    //    if (direction.magnitude < arrivingRange)
    //        //Slow down the enemy
    //        moveSpeed *= direction.magnitude / arrivingRange;
    //    else
    //        //Ramp the chase 
    //        moveSpeed = maxSpeed;
    //    direction.Normalize();//Make it unit vector of 1
    //    Turn(direction);
    //}
    private void Turn(Vector3 direction)
    {
        direction = ObstacleAvoidance(direction);//Alter initial dirn
        float turn = rotationSpeed * Time.deltaTime;
        //Calculate the amount of rotation needed
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //Apply this rotation to the rigidbody
        Quaternion rot1 = Quaternion.LookRotation(direction);
        Quaternion rot2 = Quaternion.Slerp(transform.rotation, rot1,
            rotationSpeed * Time.deltaTime);
        m_RigidBody.MoveRotation(rot2);
    }
    private void Move()
    {
        Vector3 movement = transform.forward * Time.deltaTime;
        movement *= moveSpeed;//add speed

        //There are 3 kind of movement techniques
        //1. Move the RigidBody using translation
        m_RigidBody.MovePosition(m_RigidBody.position + movement);
        m_Animator.SetFloat("Forward", animSpeed, 0.1f, Time.deltaTime);
        //2. Move the RigidBody using Force (DO NOT USE Time.deltaTime!!)
        //m_RigidBody.AddForceAtPosition(transform.forward*10,Vector3.zero);
        //3. Move the transform (Cannot use if rigidbody is present!!!!)
        //this.transform.Translate(movement);
    }
    //Obstacle avoidance return a direction vector
    //It alter the default direction if there is an obstacle 
    private Vector3 ObstacleAvoidance(Vector3 direction)
    {
        Vector3 movement = transform.forward;
        Ray ray = new Ray(transform.position + transform.up,
                            transform.forward);
        //Avoiding the layer
        int layer1 = 1 << LayerMask.NameToLayer("Player");
        int layer2 = 1 << LayerMask.NameToLayer("Terrain");
        int layer = layer1;//| layer2;
        layer = ~layer;
        RaycastHit hitInfo;
        switch (avoidanceState)
        {
            case AvoidState.SINGLE_RAY:
                if (Physics.Raycast(ray, out hitInfo, sightDistance, layer))
                {//there is obstacle 10 m ahead
                 //Turn away from obstacle towards the normal
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                break;
            case AvoidState.DOUBLE_RAY:
                ray = new Ray(transform.position + transform.up + transform.right,
                            transform.forward);//RH Ray
                if (Physics.Raycast(ray, out hitInfo, sightDistance, layer))
                {
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                ray = new Ray(transform.position + transform.up - transform.right,
                            transform.forward);//LH Ray
                if (Physics.Raycast(ray, out hitInfo, sightDistance, layer))
                {
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                break;
            case AvoidState.TRIPLE_RAY:
                ray = new Ray(transform.position + transform.up,
                            transform.forward + transform.right * Mathf.Tan(Mathf.Deg2Rad * avoidanceAngle));//RH Ray
                if (Physics.Raycast(ray, out hitInfo, peripheralDist, layer))
                {
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                ray = new Ray(transform.position + transform.up,
                            transform.forward);//Center Ray
                if (Physics.Raycast(ray, out hitInfo, sightDistance, layer))
                {
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                ray = new Ray(transform.position + transform.up,
                            transform.forward - transform.right * Mathf.Tan(Mathf.Deg2Rad * avoidanceAngle));//LH Ray
                if (Physics.Raycast(ray, out hitInfo, peripheralDist, layer))
                {
                    movement += hitInfo.normal * sightDistance;
                    return movement; //return new direction if obstacle
                }
                break;
        }
        return direction;//Maintain origin direction if no obstacle
    }
    //private void TurnTurret()
    //{
    //    turret.LookAt(target);
    //    //Check LOS (Line-of-sight)
    //    RaycastHit hitInfo;

    //    bool LOS = Physics.Raycast(
    //        transform.position + transform.up,//origin
    //        target.position - transform.position,//direction
    //        out hitInfo);//hit point information
    //    if (LOS && hitInfo.collider.CompareTag("Player"))
    //        Fire();
    //}
    //private void Fire()
    //{
    //    //ref: https://unity3d.com/learn/tutorials/projects/space-shooter/shooting-shots
    //    if (shootTimer < shootCooldown)
    //    {
    //        return;
    //    }
    //    Debug.Log("Enemy Fire");
    //    shootTimer = 0; //Reset the time when a shot is fired
    //    Rigidbody rb;
    //    //Instatiate the shell
    //    rb = Instantiate<Rigidbody>(m_Shell,
    //                m_FireTransform.position,
    //                m_FireTransform.rotation);
    //    //2 ways to launch the shell
    //    //1. Use velocity
    //    rb.velocity = 15f * m_FireTransform.forward;
    //    //2. Use Force
    //    //rb.AddForce(m_FireTransform.forward * 10000);

    //}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.color = new Color(1.0f, 1.0f, 0, 0.5f);
       // Gizmos.DrawSphere(transform.position, arrivingRange);
        Gizmos.color = Color.red;
        if (target)
            Gizmos.DrawRay(transform.position + transform.up,
                target.position - transform.position);
        //Draw our raycast
        switch (avoidanceState)
        {
            case AvoidState.SINGLE_RAY:
                Gizmos.DrawLine(
                    transform.position + transform.up,
                    transform.position + transform.up +
                    transform.forward * sightDistance);
                break;
            case AvoidState.DOUBLE_RAY:
                Gizmos.DrawLine( //RH line
                    transform.position + transform.up + transform.right,
                    transform.position + transform.up + transform.right +
                    transform.forward * sightDistance);
                Gizmos.DrawLine( //LH line
                    transform.position + transform.up - transform.right,
                    transform.position + transform.up - transform.right +
                    transform.forward * sightDistance);
                break;
            case AvoidState.TRIPLE_RAY:
                Gizmos.DrawLine( //RH line
                    transform.position + transform.up,
                    transform.position + transform.up + (transform.right * Mathf.Tan(Mathf.Deg2Rad * avoidanceAngle) +
                    transform.forward).normalized * peripheralDist);
                Gizmos.DrawLine( //Center line
                    transform.position + transform.up,
                    transform.position + transform.up +
                    transform.forward * sightDistance);
                Gizmos.DrawLine( //LH line
                    transform.position + transform.up,
                    transform.position + transform.up + (-transform.right * Mathf.Tan(Mathf.Deg2Rad * avoidanceAngle) +
                    transform.forward).normalized * peripheralDist);
                break;
        }
    }
}
