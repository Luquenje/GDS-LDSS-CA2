using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public TextMeshProUGUI HPText;
    public Slider manaBar;
    public TextMeshProUGUI manaText;
    public PlayerManager player;
    public ManaManager mana;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.maxValue = player.playerMaxHealth;
        healthBar.value = player.playerCurrentHealth;
        HPText.text = player.playerCurrentHealth + "/" + player.playerMaxHealth;
        manaBar.maxValue = mana.playerMaxMana;
        manaBar.value = mana.playerCurrentMana;
        manaText.text = mana.playerCurrentMana + "/" + mana.playerMaxMana;

    }
}
