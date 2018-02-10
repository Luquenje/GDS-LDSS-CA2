using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().HurtPlayer(damageToGive);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().HurtPlayer(damageToGive);
        }
    }

}
