using UnityEngine;
using System.Collections;
using System;

public class Guard : Enemy {

    // Update is called once per frame
    protected override void Update ()
    {

    }

    public override void MoveEnemy()
    {
        Debug.Log("moveEnemy()");
        Debug.Log("Normal transform.forward: " + (transform.localPosition + transform.forward).z
            + ", Double Transform.forward: " + (transform.localPosition + transform.forward).z * 2);

        // if player detected
        if ((int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward).x)
                && (int)Mathf.Round(player.transform.position.z) == (int) Mathf.Round ( (transform.localPosition + transform.forward*2).z ) )
        {
            transform.localPosition += transform.forward * 2;
            //Destroy(player);
            GameManager.instance.GameOver();
        }
    }
}
