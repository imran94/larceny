using UnityEngine;
using System.Collections;
using System;

public class Enemy : MovingObject {

    private Animator animator;
    private Transform target;

    public bool moveHorizontally = false;
    public bool moveVertically = false;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
        //target = GameObject.FindGameObjectsWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int zDir)
    {
        base.AttemptMove<T>(xDir, zDir);
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int zDir = 0;

        if (moveHorizontally)
            xDir++;
        if (moveVertically)
            zDir++;

        AttemptMove<Player>(xDir, zDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;
    }
}
