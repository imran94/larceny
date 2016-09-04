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
        // if player detected
        if ((int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward).x)
                && (int)Mathf.Round(player.transform.position.z) == (int) Mathf.Round ( (transform.localPosition + transform.forward*2).z ) )
        {
            moving = true;

            transform.localPosition += transform.forward * 2;
            //Destroy(player);
            //GameManager.instance.GameOver();

            moving = false;
        }
    }
}
