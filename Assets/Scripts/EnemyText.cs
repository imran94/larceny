using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyText : MonoBehaviour {
    //reference the text component
    Text text;
    public GameObject EnemyTxt;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
        Debug.Log("text == null: " + (text == null));
	}

	// Update is called once per frame
	void Update () {
        if (text == null) return;

        int enemyAmt = GameManager.instance.enemies.Count;
        int numLeft = GameManager.instance.enemies.Count - GameManager.instance.noOfEnemiesKilled();

        if (GameManager.instance.enemies.Count > 0)
        {
            EnemyTxt.SetActive(true);
            text.text = "Enemies Left: " + numLeft + "/" + enemyAmt;
        }
        else
        {
            EnemyTxt.SetActive(false);
        }
	}
}
