﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
    public static int level = 1;
    public static int maxLevel = 7;

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}

    public static 
        bool checkMaxLevel()
    {
        if (level == maxLevel)
            return true;

        return false;
    }
}
