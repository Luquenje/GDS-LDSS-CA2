using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Special Ability/Ability1"))]
public class Ability1Config : SpecialAbility {

    [Header("Specific")]
    [SerializeField] float extraDmg = 10f;

    public override void AttachComponentTo(GameObject gameObjectToAttachTo)
    {
        var behaviourComponent = gameObjectToAttachTo.AddComponent<Ability1Behaviour>();
        behaviourComponent.SetConfig(this);
        behaviour = behaviourComponent;
        //return behaviourComponent;
    }

    public float GetExtraDmg()
    {
        return extraDmg;
    }
}
