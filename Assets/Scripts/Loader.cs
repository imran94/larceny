using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
    public static int level = 6;
    public static bool maxLevel = false;

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}

    bool checkMaxLevel()
    {
        if (level == 7)
        {
            maxLevel = true;
        }
        return maxLevel;
    }
}
