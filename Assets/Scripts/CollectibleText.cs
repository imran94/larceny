using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectibleText : MonoBehaviour {
    //Reference to text component
    Text text;

    public GameObject CollectibleTxt;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (text == null) return;

        if (GameManager.instance.collectibleExists == true)
        {
            Debug.Log("Collectible exists");
            CollectibleTxt.SetActive(true);
            if (GameManager.instance.CollectiblePickedUp == false)
            {
                text.text = "Collectibles: 0/1";
            }
            else
            {
                text.text = "Collectible: 1/" + 1;
            }
        }
        else
            CollectibleTxt.SetActive(false);
	}
}
