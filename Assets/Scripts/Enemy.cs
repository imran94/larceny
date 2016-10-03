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
        {
            //transform.rotation = new Quaternion(transform.rotation.x,
            //                                    Mathf.Round(transform.rotation.y),
            //                                    transform.rotation.z,
            //                                    0);
            base.Update();
        }
    }

    protected override void AttemptMove<T>(int xDir, int zDir)
    {
        base.AttemptMove<T>(xDir, zDir);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.colliding) return;

        //if (collision.gameObject.tag == "Player")
        //    Debug.Log("Enemy collision, playersturn: " + GameManager.instance.playersTurn +
        //        ", enemiesMoving: " + GameManager.instance.enemiesMoving);

        if (collision.gameObject.tag == "Enemy")
        {
            transform.position += new Vector3(0f, 1f, 0f);
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if ((!GameManager.instance.playersTurn || GameManager.instance.enemiesMoving)
            && collision.gameObject.tag == "Player")
        {
            GameManager.instance.colliding = true;
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
