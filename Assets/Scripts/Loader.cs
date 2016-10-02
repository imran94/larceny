using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
<<<<<<< HEAD
    public static int level = 1;
    public static int maxLevel = 7;
=======
    public static int level = 7;
    public const int maxLevel = 7;
>>>>>>> origin/master

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}

    public static 
        bool checkMaxLevel()
    {
<<<<<<< HEAD
        if (level == maxLevel)
            return true;

        return false;
=======
        return level == maxLevel;
>>>>>>> origin/master
    }
}
