using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MovingObject
{
    protected GameObject player;

    protected override void Start()
    {
        player = GameObject.Find("Player");

        if (player == null)
            player = GameObject.Find("Player(Clone)");

        base.Start();
    }

    protected override void Update()
    {
        if (!GameManager.instance.enemiesMoving)
            base.Update();
    }

    protected override void AttemptMove<T>(int xDir, int zDir)
    {
        base.AttemptMove<T>(xDir, zDir);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.colliding) return;

        if (!GameManager.instance.playersTurn && GameManager.instance.enemiesMoving
            && collision.gameObject.name == "Player")
        {
            GameManager.instance.colliding = true;
            Debug.Log("Enemy collision, playersturn: " + GameManager.instance.playersTurn +
                ", enemiesMoving: " + GameManager.instance.enemiesMoving);
            StartCoroutine(GameManager.instance.GameOver());
        }
    }

    protected bool playerDetected()
    {
        return (int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward * 2).x)
                && (int)Mathf.Round(player.transform.position.z) == (int)Mathf.Round((transform.localPosition + transform.forward * 2).z);
    }

    abstract public bool MoveEnemy();
}
