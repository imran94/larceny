using UnityEngine;
using System.Collections;
using System;

public class Patrol : Enemy {

	
	protected override void Update ()
    {
        //transform.position = new Vector3((Mathf.Round(transform.position.x)), transform.position.y, Mathf.Round(transform.position.z));
    }

    public override void MoveEnemy()
    {
        Vector3 end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        if (TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            Vector3 moveTo = transform.localPosition + transform.forward * 2;
            Vector3 distance = moveTo - transform.position;
            StartCoroutine(SmoothMovement((int)distance.x, (int)distance.z, moveTo));
        }
        else
        {
            Rotate(180f);
        }
    }
}
