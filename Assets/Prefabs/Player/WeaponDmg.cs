using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDmg : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] bool isAttacking;
    [SerializeField] float damageToGive;

    // Use this for initialization
    void Start()
    {
        //playerManager = GetComponent<PlayerManager>();
        //isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking = player.GetComponent<PlayerManager>().GetAttackingState();
        damageToGive = player.GetComponent<PlayerManager>().GetCurrentAttack();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isAttacking)
        {
            other.gameObject.GetComponent<Enemy>().HurtEnemy(damageToGive);
        }
        if (other.gameObject.tag == "Boss" && isAttacking)
        {
            other.gameObject.GetComponent<Boss>().HurtEnemy(damageToGive);
        }
    }
}