using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public static int level = 8;
    public const int maxLevel = 8;

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}

    public static 
        bool checkMaxLevel()
    {
        return level >= maxLevel;
    }
}
