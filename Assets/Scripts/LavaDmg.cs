using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDmg : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().SetCurrentHealth(0);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
