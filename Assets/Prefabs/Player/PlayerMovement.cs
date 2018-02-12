using System;
using UnityEngine;
//using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (PlayerMovementControl))]
public class PlayerMovement : MonoBehaviour
{
    //bool isInDirectMode = true;

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 4f;

    PlayerMovementControl m_Character = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
    //Vector3 currentDestination, clickPoint;
    AIControl aiControl = null;

    GameObject walkTarget = null;
    Animator animator;

    public static bool isMovable = true;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<PlayerMovementControl>();
        //currentDestination = transform.position;
        //walkTarget = new GameObject("walkTarget");
        animator = GetComponent<Animator>();
        aiControl = GetComponent<AIControl>();
        //cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    //void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    //{
    //    switch (layerHit)
    //    {
    //        case enemyLayerNumber:
    //            GameObject enemy = raycastHit.collider.gameObject;
    //            aiControl.SetTarget(enemy.transform);
    //            break;
    //        case walkableLayerNumber:
    //            walkTarget.transform.position = raycastHit.point;
    //            aiControl.SetTarget(walkTarget.transform);
    //            break;
    //        default:
    //            return;
    //    }

    //}

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    isInDirectMode = !isInDirectMode;
        //    currentDestination = transform.position;
        //}

        //if (isInDirectMode)
        //{
            ProcessDirectMovement(isMovable);
        //}
        

    }

    private void ProcessDirectMovement(bool movable)
    {
        if (movable)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

            m_Character.Move(m_Move, false);
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            //animator
        }
    }

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        clickPoint = cameraRaycaster.hit.point;
    //        switch (cameraRaycaster.currentLayerHit)
    //        {
    //            case CameraRaycaster.Layer.Walkable:
    //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);

    //                break;
    //            case CameraRaycaster.Layer.Enemy:
    //                print("enemy");
    //                currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
    //                break;
    //            default:
    //                print("edge");
    //                return;
    //        }

    //    }
    //    WalkToDestination();
    //}

    //private void WalkToDestination()
    //{
    //    var playerToClick = currentDestination - transform.position;
    //    if (playerToClick.magnitude >= 0)
    //    {
    //        m_Character.Move(playerToClick, false, false);
    //    }
    //    else
    //    {
    //        m_Character.Move(Vector3.zero, false, false);
    //    }
    //}

    //Vector3 ShortDestination(Vector3 destination, float shortening)
    //{
    //    Vector3 reductionVector = (destination - transform.position).normalized * shortening;
    //    return destination - reductionVector;
    //}
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawLine(transform.position, currentDestination);
    //    Gizmos.DrawSphere(currentDestination, 0.1f);
    //    Gizmos.DrawSphere(clickPoint, 0.1f);
    //}
}

