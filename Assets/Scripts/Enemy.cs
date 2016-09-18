using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MovingObject
{

    private Animator animator;

    public bool moveHorizontally = false;
    public bool moveVertically = false;

    protected GameObject player;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int zDir)
    {
        base.AttemptMove<T>(xDir, zDir);
    }

    void onCollisionEnter(Collision collision)
    {
        if (!GameManager.instance.playersTurn && collision.gameObject.name == "Player")
        {
            Debug.Log("Collided with player");
            GameManager.instance.GameOver();
        }
    }

    abstract public void MoveEnemy();
}
