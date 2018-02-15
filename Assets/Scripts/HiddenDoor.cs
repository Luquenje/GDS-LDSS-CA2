using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour {

    public GameObject hiddenWall;
    public GameObject nxLevel;
    public GameObject UI;

	// Use this for initialization
	void Start () {
        //hiddenWall = GameObject.FindGameObjectWithTag("HiddenWall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hiddenWall.transform.position = new Vector3(0, -50, 0);
            nxLevel.SetActive(true);
            UI.SetActive(true);
            Destroy(UI, 2f);
        }
    }
}
