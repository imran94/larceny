using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        offset = new Vector3(0, 3f, 0);
	}
	
	void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
	}
}
