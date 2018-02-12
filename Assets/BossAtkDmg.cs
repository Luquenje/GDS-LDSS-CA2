using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtkDmg : MonoBehaviour {

    [SerializeField] GameObject enemy;
    [SerializeField] bool enemyIsAttacking;
    [SerializeField] int damageToGive = 1;

    // Use this for initialization
    void Start()
    {
        //playerManager = GetComponent<PlayerManager>();
        //isAttacking = false;
        enemy = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
            enemyIsAttacking = enemy.GetComponent<Boss>().Attacking();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyIsAttacking)
        {
            other.gameObject.GetComponent<PlayerManager>().HurtPlayer(damageToGive/* * 3 * (int)Time.deltaTime*/);
        }
    }
}
