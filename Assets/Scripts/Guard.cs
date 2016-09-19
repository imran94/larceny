﻿using UnityEngine;
using System.Collections;
using System;

public class Guard : Enemy {

    // Update is called once per frame
    protected override void Update ()
    {

    }

    public override void MoveEnemy()
    {
        Vector3 end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        if (TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            // if player detected
            if ((int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward * 2).x)
                    && (int)Mathf.Round(player.transform.position.z) == (int)Mathf.Round((transform.localPosition + transform.forward * 2).z))
            {
                Debug.Log("Enemy moving: " +
                    "playersTurn: " + GameManager.instance.playersTurn +
                    ", enemiesMoving: " + GameManager.instance.enemiesMoving);

                //transform.localPosition += transform.forward * 2;
                Vector3 moveTo = transform.localPosition + transform.forward * 2;
                Vector3 distance = moveTo - transform.position;
                StartCoroutine(SmoothMovement((int)distance.x, (int)distance.z, moveTo));
            }
        }
    }
}
