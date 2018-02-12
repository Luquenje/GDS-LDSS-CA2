using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    Animator animator;
    private PlayerManager playerManager;
    float enemyCurrentHp;
    float bossCurrentHp;
    [SerializeField] bool isBoss;

    public int expToGive;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isBoss)
        {
            bossCurrentHp = GetComponent<Boss>().healthAsPercentage;
            if (bossCurrentHp <= 0)
            {
                playerManager.AddExp(expToGive);
                Destroy(gameObject);
            }
        }
        else
        {
            enemyCurrentHp = GetComponent<Enemy>().healthAsPercentage;
            if (enemyCurrentHp <= 0)
            {
                playerManager.AddExp(expToGive);
                Destroy(gameObject);
            }
        }
        

        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (stateInfo.IsName("Dead")){
        //    //playerStat.AddExperience(expToGive); //<-- not working atm
        //    //Destroy(gameObject);
        //}
        
       
    }


}
