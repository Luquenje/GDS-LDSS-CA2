using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    Animator animator;
    private PlayerManager playerManager;
    float enemyCurrentHp;

    public int expToGive;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
        enemyCurrentHp = GetComponent<Enemy>().healthAsPercentage;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Dead")){
            //playerStat.AddExperience(expToGive); //<-- not working atm
            //Destroy(gameObject);
        }
        if (enemyCurrentHp <= 0)
        {
            playerManager.AddExp(expToGive);
            Destroy(gameObject);
        }
    }


}
