using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public Vector3 lvlCenter;

    int x;
    int z;
	// Use this for initialization
	void Start ()
    {
        offset = new Vector3(0f, 5f, -3f);

        x = GameManager.instance.tileScript.mapSizeX / 2;
        z = GameManager.instance.tileScript.mapSizeZ / 3;
        lvlCenter = new Vector3(x, 0f, z);

        transform.position = lvlCenter + offset;
    }
}
