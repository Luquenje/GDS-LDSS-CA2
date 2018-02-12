using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text HPText;
    public Slider manaBar;
    public Text manaText;
    public PlayerManager player;
    public ManaManager mana;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.maxValue = player.playerMaxHealth;
        healthBar.value = player.playerCurrentHealth;
        HPText.text = "HP: " + player.playerCurrentHealth + "/" + player.playerMaxHealth;
        manaBar.maxValue = mana.playerMaxMana;
        manaBar.value = mana.playerCurrentMana;
        manaText.text = "HP: " + mana.playerCurrentMana + "/" + mana.playerMaxMana;

    }
}
