using UnityEngine;
using System.Collections;
using System;

public class Patrol : Enemy {

	
	//protected override void Update ()
    //{
        //transform.position = new Vector3((Mathf.Round(transform.position.x)), transform.position.y, Mathf.Round(transform.position.z));
    //}

    public override bool MoveEnemy()
    {
        Vector3 end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        if (TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            Vector3 moveTo = transform.localPosition + transform.forward * 2;
            Vector3 distance = moveTo - transform.position;
            StartCoroutine(SmoothMovement((int)distance.x, (int)distance.z, moveTo));
            //transform.position = moveTo;

            

            return true;
        }

        return false;
    }

    private float getRemainingDistance(Vector3 end)
    {
        return Mathf.Abs((rb.position - end).z);
    }

    private IEnumerator movement(Vector3 end)
    {
        Debug.Log("Remaining Distance: " + getRemainingDistance('z', end));
        while (getRemainingDistance('z', end) > 0.001f)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
            rb.MovePosition(newPosition);

            yield return null;
        }

        end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        Debug.Log("Done moving");
        if (!TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            StartCoroutine(Rotate(180f));
        }
    }
}
