using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject tileMap;
<<<<<<< HEAD
    public static int level = 1;
=======
    public static int level = 5;
>>>>>>> refs/remotes/origin/master

	// Use this for initialization
	void Awake () 
	{
        //tileMap.generate ();

        // if (GameManager.instance == null)
        //Instantiate (gameManager);

        DontDestroyOnLoad(this.gameObject);
	}


    // Update is called once per frame
    void Update () {
	
	}
}
