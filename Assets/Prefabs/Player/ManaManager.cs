using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

    public int playerMaxMana = 100;
    public int playerCurrentMana;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isManaAvailable(int amount)
    {
        return amount <= playerCurrentMana;
    }

    public void UseMana(int manaToUse)
    {
        playerCurrentMana -= manaToUse;
    }

    public void SetMaxMana()
    {
        playerCurrentMana = playerMaxMana;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ManaPickup")
        {
            playerCurrentMana += 20;
            if (playerCurrentMana >= playerMaxMana)
            {
                playerCurrentMana = playerMaxMana;
            }

            Destroy(other.gameObject);
        }
    }
}