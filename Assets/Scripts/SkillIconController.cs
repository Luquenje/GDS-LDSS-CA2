using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconController : MonoBehaviour {

    public Image skill1CoolDownImg;
    public float skill1CoolDown = 3f;//ability 1 CD is 3sec
    bool skill1IsCoolDown;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
    }
}
