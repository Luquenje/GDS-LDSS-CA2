using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

	public GameObject gameStartedPosition;
	public GameObject characterSelectPosition;

	private bool reached_GameStartedPosition;

	private bool reached_CharacterSelectPosition = true;
	private bool canClick;
	private bool backToMainMenu;

	// EASIER WAY
	private List<GameObject> positions = new List<GameObject>();
	// EASIER WAY

	void Awake() {
		positions.Add (gameStartedPosition);
	}

	void Update () {
//		MoveToGameStartedPosition ();
//		MoveToCharacterSelectMenu ();
//
//		MoveBackToMainMenu ();

		MoveToPosition ();
	}

	void MoveToPosition() {
		if (positions.Count > 0) {

			transform.position = Vector3.Lerp (transform.position, 
				positions[0].transform.position, 1f * Time.deltaTime);

			transform.rotation = Quaternion.Lerp (transform.rotation,
				positions[0].transform.rotation, 1f * Time.deltaTime);
		}
	}

	public void ChangePosition(int index) {
		positions.RemoveAt (0);

		if (index == 0) {
			positions.Add (gameStartedPosition);
		} else {
			positions.Add (characterSelectPosition);
		}
	}

	void MoveToGameStartedPosition() {
		if (!reached_GameStartedPosition) {
			if (Vector3.Distance (transform.position, gameStartedPosition.transform.position) < 0.2f) {
				reached_GameStartedPosition = true;
				canClick = true;
			}
		}

		if (!reached_GameStartedPosition) {
			transform.position = Vector3.Lerp (transform.position, gameStartedPosition.transform.position,
				1f * Time.deltaTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, gameStartedPosition.transform.rotation,
				1f * Time.deltaTime);
		}
	}

	void MoveToCharacterSelectMenu() {
		if (!reached_CharacterSelectPosition) {
			transform.position = Vector3.Lerp (transform.position, 
				characterSelectPosition.transform.position,
				1f * Time.deltaTime);
			
			transform.rotation = Quaternion.Lerp (transform.rotation, 
				characterSelectPosition.transform.rotation,
				1f * Time.deltaTime);
		}

		if (!reached_CharacterSelectPosition) {
			if (Vector3.Distance (transform.position, characterSelectPosition.transform.position) < 0.2f) {
				reached_CharacterSelectPosition = true;
				canClick = true;
			}
		}
	}

	void MoveBackToMainMenu() {
		if (backToMainMenu) {
			if (Vector3.Distance (transform.position, gameStartedPosition.transform.position) < 0.2f) {
				backToMainMenu = false;
				canClick = true;
			}
		}

		if (backToMainMenu) {
			transform.position = Vector3.Lerp (transform.position, gameStartedPosition.transform.position,
				1f * Time.deltaTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, gameStartedPosition.transform.rotation,
				1f * Time.deltaTime);
		}
	}

	public bool ReachedCharacterSelectPosition {
		get { return reached_CharacterSelectPosition; }
		set { reached_CharacterSelectPosition = value; }
	}

	public bool CanClick {
		get { return canClick; }
		set { canClick = value; }
	}

	public bool BackToMainMenu {
		get { return backToMainMenu; }
		set { backToMainMenu = value; }
	}

} // class






































