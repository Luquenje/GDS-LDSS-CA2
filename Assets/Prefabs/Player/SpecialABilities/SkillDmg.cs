using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDmg : MonoBehaviour {

    public LayerMask enemyLayer;
    public float damage = 5f;
    public float radius = 2f;

    private Enemy enemyHealth;
    private bool collided;

    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
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

    }
}
