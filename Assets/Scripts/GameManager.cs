﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = 1f;
	public static GameManager instance = null;

	private GameObject player;
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
		else if (instance != this)
			// Then destroy this to enforce a singleton pattern
			Destroy (this);

        // set this to not be destroyed when reloading scene
        DontDestroyOnLoad(this);

        player = GameObject.Find ("Player");

        enemies = new List<Enemy>();
        
        //Get a component reference to the attached TileMap script
        tileMap = Instantiate(Resources.Load("TileMap")) as GameObject;
        tileScript = tileMap.GetComponent<TileMap>();

        InitGame ();
	}

	void InitGame ()
	{
        generateLevel(Loader.level);
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }

        if (player == null)
        { 
            //Time.timeScale = 0;
            //Debug.Log("Player has been destroyed");

            //Destroy(GameObject.Find("TileMap"));

            //InitGame();
            //return;
            //int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(sceneIndex);

            //resetLvl();
        }

        if (playersTurn || enemiesMoving)
            return;

        if (player != null)
        {
            int x = (int)Mathf.Round(player.transform.position.x);
            int z = (int)Mathf.Round(player.transform.position.z);

            if (TileMap.tiles[x, z].name == "Finish")
            {
                levelComplete();
            }
        }

        StartCoroutine(MoveEnemies());
	}

    public void AddEnemiesToScript(Enemy script)
    {
        //enemies.Add(script);
    }

	public void levelComplete()
	{
        resetLvl();
	}

    public void GameOver()
    {
        float currentTime = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log("Player has been destroyed");

        Destroy(GameObject.Find("TileMap"));

        //InitGame();
        //return;
        //int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(sceneIndex);

        resetLvl();
        Time.timeScale = currentTime;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        if (enemies.Count == 0)
        {
            Debug.Log("No enemies");
            yield return new WaitForSeconds(turnDelay);
        }

        yield return new WaitForSeconds(turnDelay);

        Debug.Log(enemies.Count);
        foreach (Enemy enemy in enemies.ToArray())
        {
            if (enemy != null)
            {
                Debug.Log("moving enemiy");
                enemy.MoveEnemy();
                yield return new WaitForSeconds(enemy.moveTime);
            }
        }

        playersTurn = true;
        enemiesMoving = false;
    }

    public void generateLevel(int level)
    {
        if (player == null)
            player = Instantiate(Resources.Load("Player")) as GameObject;

        tileScript.generateLevel(level);

        switch (level)
        {
            case 1:
                generateLevel1();
                break;
            case 2:
                generateLevel2();
                break;
            case 3:
                generateLevel3();
                break;
            case 4:
                generateLevel4();
                break;
            case 5:
                generateLevel5();
                break;

        }
    }

    void resetLvl()
    {
        //player.transform.position = new Vector3(0f, 3f, 0f);

        //GameObject go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 8f), new Quaternion(0, 180, 0, 0));
        //GameObject go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 8f), Quaternion.identity);
        //Enemy enemy = go.GetComponent<Guard>();
        //enemies.Add(enemy);
        //resetLvl();

        generateLevel(Loader.level);
    }

    void generateLevel1() { }
    void generateLevel2() { }
    void generateLevel3() { }

    void generateLevel4()
    {
        player.transform.position = new Vector3(1f, 3f, 1f);

        foreach (Enemy e in enemies)
        {
            if (e != null)
                Destroy(e.gameObject);
        }
        enemies.Clear();

        instantiateEnemy("Patrol", 1f, 7f, 90f);
        instantiateEnemy("Guard", 3f, 3f, 180f);
        instantiateEnemy("Guard", 3f, 5f, 180f);
        instantiateEnemy("Guard", 5f, 3f, -90f);
        instantiateEnemy("Guard", 5f, 5f, -90f);
        instantiateEnemy("Guard", 7f, 1f, 0f);
        instantiateEnemy("Guard", 7f, 5f, 180f);
    }

    void generateLevel5() { }

    void instantiateEnemy(string type, float x, float z, float angleY)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(type), new Vector3(x, 3f, z), Quaternion.identity);
        go.transform.Rotate(0f, angleY, 0f);
        Enemy enemy = go.GetComponent(type) as Enemy;
        enemies.Add(enemy);
    }
}
