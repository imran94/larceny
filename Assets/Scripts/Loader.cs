using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
<<<<<<< HEAD
    public static int level = 6;
=======
    public static int level = 1;
>>>>>>> 2a4a909a4a9ae66bd4582741e333a77699edb63d
    public const int maxLevel = 7;

	void Awake () 
	{
        // if (GameManager.instance == null)
        //	Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}
}
