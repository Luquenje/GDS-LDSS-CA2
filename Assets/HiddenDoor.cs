using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour {

    [SerializeField] GameObject hiddenWall;
    public GameObject UI;

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
            Destroy(hiddenWall);
            UI.SetActive(true);
            Destroy(UI, 2f);
        }
    }
}
