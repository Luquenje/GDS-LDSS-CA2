﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonPanel, characterSelectPanel, createCharacterPanel, optionsPanel;

	private MainMenuCamera mainMenuCamera;

	void Awake () {
		mainMenuCamera = Camera.main.GetComponent<MainMenuCamera> ();
	}
	
	public void PlayGame() {

		mainMenuCamera.ChangePosition (1);
		buttonPanel.SetActive (false);
		characterSelectPanel.SetActive (true);

//		if (mainMenuCamera.CanClick) {
//			mainMenuCamera.CanClick = false;
//
//			buttonPanel.SetActive (false);
//			characterSelectPanel.SetActive (true);
//
//			mainMenuCamera.ReachedCharacterSelectPosition = false;
//		}
	}

	public void BackToMainMenu() {
		mainMenuCamera.ChangePosition (0);
		buttonPanel.SetActive (true);
		characterSelectPanel.SetActive (false);

//		if (mainMenuCamera.CanClick) {
//			mainMenuCamera.CanClick = false;
//			mainMenuCamera.BackToMainMenu = true;
//
//			buttonPanel.SetActive (true);
//			characterSelectPanel.SetActive (false);
//		}
	}

	public void StartGame() {
		SceneLoader.instance.LoadLevel ("Village");
	}

	public void CreateCharacter() {
		characterSelectPanel.SetActive (false);
		createCharacterPanel.SetActive (true);
	}

	public void Accept() {
		characterSelectPanel.SetActive (true);
		createCharacterPanel.SetActive (false);
	}

	public void Cancel() {
		characterSelectPanel.SetActive (true);
		createCharacterPanel.SetActive (false);
	}

	public void OptionsPanel() {
		optionsPanel.SetActive (true);
		buttonPanel.SetActive (false);
	}

	public void CloseOptionsPanel() {
		optionsPanel.SetActive (false);
		buttonPanel.SetActive (true);
	}

	public void SetQuality() {
		ChangeQualityLevel ();
	}

	public void SetResolution() {
		ChangeResolution ();
	}

	void ChangeQualityLevel() {
		string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

		switch (level) {

		case "Low":
			QualitySettings.SetQualityLevel (0);
			break;

		case "Normal":
			QualitySettings.SetQualityLevel (1);
			break;

		case "High":
			QualitySettings.SetQualityLevel (2);
			break;

		case "Ultra":
			QualitySettings.SetQualityLevel (3);
			break;

		case "No Shadows":
			if (QualitySettings.shadows == ShadowQuality.All) {
				QualitySettings.shadows = ShadowQuality.Disable;
			} else {
				QualitySettings.shadows = ShadowQuality.All;
			}
			break;
		
		}

	}

	void ChangeResolution() {
		string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

		switch (index) {
		case "0":
			Screen.SetResolution (1152, 648, true);
			break;

		case "1":
			Screen.SetResolution (1280, 720, true);
			break;

		case "2":
			Screen.SetResolution (1360, 768, true);
			break;

		case "3":
			Screen.SetResolution (1920, 1080, true);
			break;
		}

	}

} // class





































