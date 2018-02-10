using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour {

	public GameObject[] characters;
	public GameObject charPosition;

	private int knight_Warrior_Index = 0;
	private int king_Warrior_Index = 1;
	private int catGirl_Warrior_Index = 2;

	void Start () {
		characters [knight_Warrior_Index].SetActive (true);
		characters [knight_Warrior_Index].transform.position = charPosition.transform.position;
	}

	public void SelectCharacter() {
		int index = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

		TurnOffCharacters ();

		characters [index].SetActive (true);
		characters [index].transform.position = charPosition.transform.position;

		GameManager.instance.selectedCharacterIndex = index;

	}

	void TurnOffCharacters() {
		for (int i = 0; i < characters.Length; i++) {
			characters [i].SetActive (false);
		}
	}

} // class






































