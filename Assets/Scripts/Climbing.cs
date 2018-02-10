using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {
    public GameObject player;
    Animator animator;
    Rigidbody rb;
    public float v;
    private bool climable = false;
    private float climbSpeed = 0.05f;
	// Use this for initialization
	void Start () {
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();
	}
	

	void FixedUpdate () {
        Climb(climable);
	}
    
    

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "ladderBase")
    //    {
    //        Debug.Log("climb");
    //        animator.SetBool("Climb_idle", true);
    //        PlayerMovement.isMovable = false;
    //    }
    //}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.right || collision.contacts[0].normal == Vector3.left)
        {
            Debug.Log("climb");
            //animator.SetBool("Climb_idle", true);
            PlayerMovement.isMovable = false;
            climable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        climable = false;
        PlayerMovement.isMovable = true;
        rb.useGravity = true;
        
        animator.SetBool("Climb_up", false);
        player.transform.Translate(Vector3.forward*0.2f);
    }

    void Climb(bool climable)
    {
        if (climable)
        {

            v = Input.GetAxis("Vertical");
            
            //Vector3 input = new Vector3(0, 1000.0f, 0);
            //rb.AddForce(input * v);
            if (v > 0f)
            {
                player.transform.Translate(Vector3.up * v * climbSpeed);
                player.transform.Translate(Vector3.forward*0.1f);
                animator.SetBool("Climb_up", true);
            }
            if (v < 0)
            {
                player.transform.Translate(Vector3.down * Mathf.Abs(v) * climbSpeed);
                animator.SetBool("Climb_up", true);
            }

            if (v == 0)
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
            rb.useGravity = false;
            
            

        }


    }
}
