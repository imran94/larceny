﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
    public static int level = 7;
    public const int maxLevel = 7;

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}

    bool checkMaxLevel()
    {
        return level == maxLevel;
    }
}
