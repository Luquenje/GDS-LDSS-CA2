using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : MonoBehaviour {

    private float lifeTime = 2f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
