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

    }

    public override void MoveEnemy()
    {
        Debug.Log("moveEnemy()");
        Debug.Log("Normal transform.forward: " + (transform.localPosition + transform.forward).z
            + ", Double Transform.forward: " + (transform.localPosition + transform.forward).z * 2);

        if ((int)Mathf.Round(player.transform.position.x) == (int)Mathf.Round((transform.localPosition + transform.forward).x)
                && (int)Mathf.Round(player.transform.position.z) == (int) Mathf.Round ( (transform.localPosition + transform.forward*2).z ) )
        {
            Debug.Log("Detected player");
            moving = true;

            transform.localPosition += transform.forward;
            //Destroy(player);
            GameManager.instance.GameOver();

            moving = false;
        }
    }


    protected override void OnCantMove<T>(T component)
    {
    }
}
