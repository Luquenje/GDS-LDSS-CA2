using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct AbilityUseParams
{
   // public Enemy target;
    public float baseDmg;

    public AbilityUseParams(float baseDmg)
    {
        //this.target = target;
        this.baseDmg = baseDmg;
    }
}

public abstract class SpecialAbility : ScriptableObject {

    [Header("Special Ability General")]
    [SerializeField] int manaCost = 10;
    Ability1Config config;

    protected ISpecialAbility behaviour;

    abstract public void AttachComponentTo(GameObject gameObjectToAttachTo);

    public void Use(AbilityUseParams useParams)
    {
        behaviour.Use(useParams);
    }
    public void SetConfig(Ability1Config configToSet)
    {
        this.config = configToSet;
    }

    public int GetManaCost()
    {
        return manaCost;
    }
}

public interface ISpecialAbility
{
    void Use(AbilityUseParams useParams);
}
