using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NextLevel : MonoBehaviour {

    //SceneManage scene;
    //public string sceneName = "CaveLevel";
    //public int sceneIndex;
    public GameObject finishScene;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI loadingPercent;
	// Use this for initialization
	void Start () {
        //loadingPercent = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            finishScene.SetActive(true);
        }
        Cursor.visible = true;
    }
    public void LoadLv(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        finishScene.SetActive(false);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            loadingPercent.text = progress * 100f + "%";
            Debug.Log(progress);
            yield return null;
        }
    }
}
