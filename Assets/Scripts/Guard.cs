using UnityEngine;
using System.Collections;
using System;

public class Guard : Enemy {

    public Guard(Vector3 position)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("ChainMail_Knight"), position, Quaternion.identity);
    }

	// Use this for initialization
	//public override void Start () {
	
	//}
	
	// Update is called once per frame
	//public override void Update () {
	
	//}

    public override void MoveEnemy()
    {
        Move(1, 0);
    }

    protected override void OnCantMove<T>(T component)
    {
    }
}
