using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconController : MonoBehaviour {

    public Image skill1CoolDownImg;
    public float skill1CoolDown = 3f;//ability 1 CD is 3sec
    bool skill1IsCoolDown;
    public Image skill2CoolDownImg;
    public float skill2CoolDown = 6f;//ability 1 CD is 3sec
    bool skill2IsCoolDown;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skill1IsCoolDown = true;
        }
        if (skill1IsCoolDown)
        {
            skill1CoolDownImg.fillAmount += 1 / skill1CoolDown * Time.deltaTime;

            if(skill1CoolDownImg.fillAmount >= 1)
            {
                skill1CoolDownImg.fillAmount = 0;
                skill1IsCoolDown = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skill2IsCoolDown = true;
        }
        if (skill2IsCoolDown)
        {
            skill2CoolDownImg.fillAmount += 1 / skill2CoolDown * Time.deltaTime;

            if (skill2CoolDownImg.fillAmount >= 1)
            {
                skill2CoolDownImg.fillAmount = 0;
                skill2IsCoolDown = false;
            }
        }
    }
}

   

