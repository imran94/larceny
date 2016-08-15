using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject prefab = (GameObject) Instantiate (Resources.Load ("Player"));
	public Vector3 position;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPosition()
	{
	}

}
