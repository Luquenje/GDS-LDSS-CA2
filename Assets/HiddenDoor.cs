using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour {

    [SerializeField] GameObject hiddenWall;

	// Use this for initialization
	void Start () {
        hiddenWall = GameObject.FindGameObjectWithTag("HiddenWall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hiddenWall.SetActive(false);
        }
    }
}
