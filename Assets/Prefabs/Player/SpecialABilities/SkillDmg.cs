using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDmg : MonoBehaviour {

    public LayerMask enemyLayer;
    public LayerMask bossLayer;
    public float damage = 5f;
    public float radius = 2f;

    private Enemy enemyHealth;
    private Boss bossHealth;
    private bool collided;
    private bool collided1;

    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
        //HitEnemy
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider h in hits)
        {
            enemyHealth = h.GetComponent<Enemy>();
            if (enemyHealth)
            {
                collided = true;
            }
        }

        if (collided)
        {
            collided = false;
            enemyHealth.HurtEnemy(damage * 3 * Time.deltaTime);
            //Destroy(gameObject);
        }

        //HitBoss
        Collider[] hits1 = Physics.OverlapSphere(transform.position, radius, bossLayer);

        foreach (Collider h1 in hits1)
        {
            bossHealth = h1.GetComponent<Boss>();
            if (bossHealth)
            {
                collided1 = true;
            }
        }

        if (collided1)
        {
            collided1 = false;
            bossHealth.HurtEnemy(damage * 3 * Time.deltaTime);
            //Destroy(gameObject);
        }
    }
}
