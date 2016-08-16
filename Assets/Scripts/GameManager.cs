using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = 0.1f;
	public static GameManager instance = null;

    public bool playersTurn = true;

    public GameObject tileMap;
    public TileMap tileScript;

    private List<Enemy> enemies;
    private bool enemiesMoving;

    // Use this for initialization
    void Awake () 
	{
		// Check if instance already exists
		if (instance == null)
			// if not, set the instance to this	
			instance = this;
		// if instance exists, and is not this
			// else if (instance != this)
			// Then destroy this to enforce a singleton pattern
				// Destroy (gameObject);

		// set this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

        enemies = new List<Enemy>();

        //Get a component reference to the attached TileMap script
        tileMap = Instantiate(Resources.Load("TileMap")) as GameObject;
        tileScript = tileMap.GetComponent<TileMap>();

        InitGame ();
	}

	void InitGame ()
	{

        enemies.Clear();
        //tileScript.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playersTurn || enemiesMoving)
            return;

        StartCoroutine(MoveEnemies());
	}

    public void AddEnemiesToScript(Enemy script)
    {
        enemies.Add(script);
    }

    public void GameOver()
    {
        
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        foreach(Enemy enemy in enemies)
        {
            enemy.MoveEnemy();
            yield return new WaitForSeconds(enemy.moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
}
