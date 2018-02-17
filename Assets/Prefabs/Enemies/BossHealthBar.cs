using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage = null;
    [SerializeField] Boss boss;
    public GameObject golem;

    // Use this for initialization
    void Start()
    { // Different to way player's health bar finds player
        boss = golem.GetComponent<Boss>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = -(boss.healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
