using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NxtLvCave : MonoBehaviour {

    public GameObject finishScene;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI loadingPercent;
    public Transform enemy;
    private int enemyCount;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = enemy.childCount;
        if(enemyCount == 0)
        {
            finishScene.SetActive(true);
            Cursor.visible = true;
            enabled = false;
        }
        //Debug.Log(enemyCount);
    }
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        finishScene.SetActive(true);
    //    }
    //    Cursor.visible = true;
    //}
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
