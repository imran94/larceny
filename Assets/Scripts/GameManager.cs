using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = 1f;
	public static GameManager instance = null;
    public GameObject WinImg;

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
        //DontDestroyOnLoad(this);
        /** gameObject needs to be destroyed in order to eliminate the bug **/


        player = GameObject.Find ("Player");
		//player = (GameObject)Instantiate(Resources.Load("Player"));

        enemies = new List<Enemy>();
        
        //Get a component reference to the attached TileMap script
        tileMap = Instantiate(Resources.Load("TileMap")) as GameObject;
        tileScript = tileMap.GetComponent<TileMap>();

        InitGame ();
	}

	void InitGame ()
	{
        generateLevel(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }

        if (player == null)
        { 
            Time.timeScale = 0;
            Debug.Log("Player has been destroyed");

            Destroy(GameObject.Find("TileMap"));

            //InitGame();
            //return;
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }

        if (playersTurn || enemiesMoving)
            return;

        int x = (int)Mathf.Round (player.transform.position.x);
		int z = (int)Mathf.Round (player.transform.position.z);

		if (TileMap.tiles [x, z].name == "Finish") 
		{
			levelComplete ();
		}

        StartCoroutine(MoveEnemies());
	}

    public void AddEnemiesToScript(Enemy script)
    {
        //enemies.Add(script);
    }

	public void levelComplete()
	{
        //resetLvl();
        WinImg.SetActive(true);
    }

    public void GameOver()
    {
        resetLvl();
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
                // yield return new WaitForSeconds(enemy.moveTime);
            }
        }

        playersTurn = true;
        enemiesMoving = false;
    }

    public void generateLevel(int level)
    {
        tileScript.generateLevel(level);

        switch (level)
        {
            case 1:
                generateLevel1();
                break;
        }
    }

    void generateLevel1()
    {
        player.transform.position = new Vector3(0f, 3f, 0f);

        //GameObject go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 8f), new Quaternion(0, 180, 0, 0));
        //GameObject go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 8f), Quaternion.identity);
        //Enemy enemy = go.GetComponent<Guard>();
        //enemies.Add(enemy);
        resetLvl();
    }

    void resetLvl()
    {
        player.transform.position = new Vector3(0f, 3f, 0f);

        foreach (Enemy e in enemies)
        {
            if (e != null)
                Destroy(e.gameObject);
        }
        enemies.Clear();

        GameObject go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(0f, 3f, 6f), Quaternion.identity);
        go.transform.Rotate(0f, 90f, 0f);
        Enemy enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(2f, 3f, 2f), Quaternion.identity);
        go.transform.Rotate(0f, 180f, 0f);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(2f, 3f, 4f), Quaternion.identity);
        go.transform.Rotate(0f, 180f, 0f);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 2f), Quaternion.identity);
        go.transform.Rotate(0f, 90f, 0f);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(4f, 3f, 4f), Quaternion.identity);
        go.transform.Rotate(0f, 90f, 0f);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(6f, 3f, 0f), Quaternion.identity);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);

        go = (GameObject)Instantiate(Resources.Load("Guard"), new Vector3(6f, 3f, 4f), Quaternion.identity);
        go.transform.Rotate(0f, 180f, 0f);
        enemy = go.GetComponent<Guard>();
        enemies.Add(enemy);
    }
}
