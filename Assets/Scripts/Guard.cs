using UnityEngine;
using System.Collections;
using System;

public class Guard : Enemy {

    GameObject player;
    bool moving;

    // Use this for initialization
    protected override void Start()
    {
        player = GameObject.Find ("Player");
        moving = false;
    }
    
    // Update is called once per frame
    protected override void Update ()
    {
        Debug.Log("transformForward: " + (transform.localPosition + transform.forward));
        if (!moving && (int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round(transform.position.x)
                && (int)Mathf.Round(player.transform.position.z) == (int)Mathf.Round(transform.position.z))
        {
            Destroy(this);
        }
    }

    public override void MoveEnemy()
    { 
        if ((int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward).x)
                && (int)Mathf.Round(player.transform.position.z) == (int)Mathf.Round((transform.localPosition + transform.forward).z))
        {
            moving = true;
            transform.localPosition += transform.forward;
            Destroy(player);
            moving = false;
        }
    }


    protected override void OnCantMove<T>(T component)
    {
    }
}
