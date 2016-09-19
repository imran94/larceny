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
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Enemy collision: " +
                "playersTurn: " + GameManager.instance.playersTurn +
                    ", enemiesMoving: " + GameManager.instance.enemiesMoving);
        }

        if (!GameManager.instance.playersTurn && GameManager.instance.enemiesMoving
            && collision.gameObject.name == "Player")
        {
            StartCoroutine(GameManager.instance.GameOver());
        }
    }

    abstract public void MoveEnemy();
}
