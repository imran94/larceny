using UnityEngine;
using System.Collections;
using System;

public class Guard : Enemy {

    // Update is called once per frame
    //protected override void Update ()
    //{

    //}
    public AudioClip SFX_Enemy_Move;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override bool MoveEnemy()
    {
        Vector3 end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        source = GetComponent<AudioSource>();

        if (TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            // if player detected
            if (playerDetected())
            {
                //transform.localPosition += transform.forward * 2;
                Vector3 moveTo = transform.localPosition + transform.forward * 2;
                Vector3 distance = moveTo - transform.position;
                StartCoroutine(SmoothMovement((int)distance.x, (int)distance.z, moveTo));
                source.PlayOneShot(SFX_Enemy_Move, 1F);
                Debug.Log("SFM PLAYED");

                return true;
            }
        }

        return false;
    }
}
