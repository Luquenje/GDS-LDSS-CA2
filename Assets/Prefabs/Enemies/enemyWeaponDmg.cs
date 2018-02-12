using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeaponDmg : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] bool enemyIsAttacking;
    [SerializeField] int damageToGive = 15;

    // Use this for initialization
    void Start()
    {
        //playerManager = GetComponent<PlayerManager>();
        //isAttacking = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy)
        enemyIsAttacking = enemy.GetComponent<Enemy>().Attacking();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyIsAttacking)
        {
            other.gameObject.GetComponent<PlayerManager>().HurtPlayer(damageToGive * 2 * Time.deltaTime);
        }
    }
}
