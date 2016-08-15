using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public TileMap tileScript;

	// Use this for initialization
	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		tileScript = GetComponent<TileMap> ();
		InitGame ();
	}

	void InitGame ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
