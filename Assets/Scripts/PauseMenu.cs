using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject backBtn;
    public GameObject exitBtn;
    public GameObject Title;
    public GameObject Panel;
    // Use this for initialization
    void Start () {
        pauseMenuUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        backBtn.SetActive(false);
        exitBtn.SetActive(false);
        Title.SetActive(false);
        Panel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        backBtn.SetActive(true);
        exitBtn.SetActive(true);
        Title.SetActive(true);
        Panel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
