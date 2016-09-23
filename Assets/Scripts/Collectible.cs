using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	void Start ()
    {
        transform.Rotate(45f, 45f, 45f);
    }

    void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" )
        {
            GameManager.instance.CollectiblePickedUp = true;
            Destroy(this.gameObject);
        }
    }
}
