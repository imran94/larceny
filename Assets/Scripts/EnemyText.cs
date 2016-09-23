﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyText : MonoBehaviour {
    //reference the text component
    Text text;

    public GameObject EnemyTxt;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    int enemyAmt = GameManager.instance.enemies.Count;

	// Update is called once per frame
	void Update () {
        if (GameManager.instance.enemies.Count != 0)
        {
            EnemyTxt.SetActive(true);
            text.text = "Enemies: " + GameManager.instance.noOfEnem + "/" + enemyAmt;
            Debug.Log(enemyAmt);
        }
        else
        {
            EnemyTxt.SetActive(false);
        }
	}
}
