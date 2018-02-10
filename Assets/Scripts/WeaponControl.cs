using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("RPG/Weapon"))]
public class WeaponControl : ScriptableObject{

    public Transform gripTransform;

    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimationClip attackAnimation;
    [SerializeField] float minTimeBetweenHits = 1.4f;
    [SerializeField] float maxAttackRange = 3f;

    public float GetMinTimeBetweenHits()
    {
        return minTimeBetweenHits;
    }
    public float GetMaxAttackRange()
    {
        return maxAttackRange;
    }

    public GameObject GetWeaponPrefab()
    {
        return weaponPrefab;
    }

    public AnimationClip GetAttackAnimClip()
    {
        return attackAnimation;
    }

}
