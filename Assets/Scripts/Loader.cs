using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
    public static int level = 4;
    public const int maxLevel = 7;

	void Awake () 
	{
        // tileMap.generate ();

        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}
}
