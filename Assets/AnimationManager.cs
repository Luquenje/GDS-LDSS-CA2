using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    Animator animator;
    private PlayerManager playerManager;
    float enemyCurrentHp;
    float bossCurrentHp;
    [SerializeField] bool isBoss;
    public GameObject spawn;
    [SerializeField]GameObject uiSocket;

    bool isInstantiated = false;
    public int expToGive;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        //uiSocket = GameObject.FindGameObjectWithTag("UISocket");
	}
	
	// Update is called once per frame
	void Update () {
        if (isBoss)
        {
            bossCurrentHp = GetComponent<Boss>().healthAsPercentage;
            if (bossCurrentHp <= 0)
            {
                if (!isInstantiated)
                {
                    playerManager.AddExp(expToGive);
                    animator.SetBool("Dead", true);
                    isInstantiated = true;
                }
                
                Destroy(gameObject, 3f);
            }
        }
        else
        {
            enemyCurrentHp = GetComponent<Enemy>().healthAsPercentage;
            if (enemyCurrentHp <= 0)
            {
                uiSocket.SetActive(false);
                if (!isInstantiated)
                {
                    
                    playerManager.AddExp(expToGive);
                    animator.SetBool("Dead", true);
                    Instantiate(spawn, gameObject.transform.position, Quaternion.identity);
                    isInstantiated = true;
                }
                Destroy(gameObject, 3f);
            }
        }
        

        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (stateInfo.IsName("Dead")){
        //    //playerStat.AddExperience(expToGive); //<-- not working atm
        //    //Destroy(gameObject);
        //}
        
       
    }


}
