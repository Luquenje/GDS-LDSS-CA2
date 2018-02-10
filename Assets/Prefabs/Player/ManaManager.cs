using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour {

    public int playerMaxMana = 100;
    public int playerCurrentMana;

    // Use this for initialization
    void Start () {
        playerCurrentMana = playerMaxMana;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isManaAvailable(int amount)
    {
        return amount <= playerCurrentMana;
    }

    public void UseMana(int damageToGive)
    {
        playerCurrentMana -= damageToGive;
    }

    public void SetMaxMana()
    {
        playerCurrentMana = playerMaxMana;
    }
}
