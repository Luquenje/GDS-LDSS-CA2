using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1Behaviour : MonoBehaviour, ISpecialAbility {

    Ability1Config config;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetConfig(Ability1Config configToSet)
    {
        this.config = configToSet;
    }

    public void Use(AbilityUseParams useParams)
    {
        print("using ability " + config.GetExtraDmg() + " " + useParams.baseDmg);
    }
}
