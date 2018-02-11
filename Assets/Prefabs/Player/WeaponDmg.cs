using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDmg : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] bool isAttacking;
    [SerializeField] int damageToGive = 15;

    // Use this for initialization
    void Start () {
        //playerManager = GetComponent<PlayerManager>();
        //isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        isAttacking = player.GetComponent<PlayerManager>().GetAttackingState();
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isAttacking)
        {
            other.gameObject.GetComponent<Enemy>().HurtEnemy(15f);
        }
    }
}
