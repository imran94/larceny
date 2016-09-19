using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MovingObject
{
    protected GameObject player;

    protected override void Start()
    {
        player = GameObject.Find("Player");
        base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int zDir)
    {
        base.AttemptMove<T>(xDir, zDir);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.instance.playersTurn && GameManager.instance.enemiesMoving
            && collision.gameObject.name == "Player")
        {
            GameManager.instance.GameOver();
        }
    }

    abstract public void MoveEnemy();
}
