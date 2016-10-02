using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    Text text;
    public float levelStartDelay = 1.0f;
    public float turnDelay = 3f;
    public static GameManager instance = null;
    public GameObject WinImg;
    public AudioClip SFX_LevelComplete;
    public GameObject Tut1;
    public GameObject Tut2;

    private GameObject player;
    private GameObject collectible;
    private GameObject nextBtn;
    private AudioSource source;

    private Player playerScript;
    public bool playersTurn = true;
    public bool enemiesMoving;
    private bool gameOver;

    public bool collectibleExists = false;
    public bool CollectiblePickedUp = false;

    public bool colliding;

    public GameObject tileMap;
    public TileMap tileScript;

    public List<Enemy> enemies;

    // Use this for initialization
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
            // if not, set the instance to this	
            instance = this;
        // if instance exists, and is not this
        else if (instance != this)
            // Then destroy this to enforce a singleton pattern
            Destroy(this);

        // set this to not be destroyed when reloading scene
        //DontDestroyOnLoad(this);
        /** gameObject needs to be destroyed in order to eliminate the bug **/

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        nextBtn = GameObject.Find("/Canvas_Win/PauseImg");

        enemies = new List<Enemy>();

        //Get a component reference to the attached TileMap script
        tileMap = Instantiate(Resources.Load("TileMap")) as GameObject;
        tileScript = tileMap.GetComponent<TileMap>();
        tileScript.generateLevel(Loader.level);

        source = GetComponent<AudioSource>();
        SFX_LevelComplete = Resources.Load("SFX_LevelComplete") as AudioClip;

        text = GetComponent<Text>(); 

        InitGame();
    }

	public void InitGame ()
    {
        nextBtn = GameObject.Find("Canvas_Win").transform.FindChild("WinImg").transform.FindChild("NextBtn").gameObject;
        if (Loader.level >= Loader.maxLevel)
            nextBtn.SetActive(false);
        //if (canvasWin.transform.Find("NextBtn") == null)
            //Debug.Log("Could not find nextBtn");
        //else
        //{
        //    Debug.Log("Iterating through children total: " + transform.childCount);
        //    foreach (var child in transform)
        //        Debug.Log(child);
        //}

        generateLevel(Loader.level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.R)) { resetLvl(); }
        
        if (playersTurn || enemiesMoving || gameOver) return;

        if (player != null)
        {
            int x = (int)Mathf.Round(player.transform.position.x);
            int z = (int)Mathf.Round(player.transform.position.z);

            if (TileMap.tiles[x, z].name == "Finish")
            {
                levelComplete();
            }
        }

        enemiesMoving = true;

        StartCoroutine(MoveEnemies());
    }

    public bool allEnemiesKilled()
    {
        foreach (Enemy e in enemies)
        {
            if (e != null)
                return false;
        }

        return true;
    }

    public int noOfEnemiesKilled()
    {
        int i = 0;

        foreach(Enemy e in enemies)
        {
            if (e == null)
                i++;
        }

        return i;
    }

    public void levelComplete()
    {
        //resetLvl();
        Debug.Log("All Enemies Killed: " + allEnemiesKilled());

        WinImg.SetActive(true);
        source.PlayOneShot(SFX_LevelComplete, 1F);
    }

    public IEnumerator GameOver()
    {
        Debug.Log("Game Over");
        gameOver = true;
        StartCoroutine(playerScript.explode(true));
        float currentTime = Time.timeScale;
        //Time.timeScale = 0;
        //Destroy(player);

        yield return new WaitForSeconds(levelStartDelay);

        //Destroy(tileMap);

        //InitGame();
        //return;
        //int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(sceneIndex);

        resetLvl();
        //Time.timeScale = currentTime;
    }

    IEnumerator MoveEnemies()
    {
        while (playerScript.moving)
            yield return new WaitForEndOfFrame();

        bool enemyMovement = false;
        int movingEnemyIndex = 0;

        foreach (Enemy enemy in enemies.ToArray())
        {
            if (enemy != null)
            {
                if (enemy.MoveEnemy())
                {
                    enemyMovement = true;
                    movingEnemyIndex = enemies.IndexOf(enemy);
                }
            }
        }

        if (enemyMovement)
            yield return new WaitForSeconds(enemies[movingEnemyIndex].moveTime * enemies.Count);

        playersTurn = true;
        playerScript.input = true;
        enemiesMoving = false;
    }

    public void generateLevel(int level)
    {
        colliding = false;
        gameOver = false;

        if (player == null)
        {
            player = Instantiate(Resources.Load("Player")) as GameObject;
            playerScript = player.GetComponent<Player>();
        }

        if (tileMap == null)
        {
            Debug.Log("Generating tilemap");
            tileScript.generateLevel(level);
        }
        foreach (Enemy e in enemies)
        {
            if (e != null)
                Destroy(e.gameObject);
        }
        enemies.Clear();

        collectibleExists = CollectiblePickedUp = false;
        if (collectible != null)
            Destroy(collectible);

        generateFromLoader();
    }

    void generateFromLoader()
    {
        switch (Loader.level)
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
            case 6:
                generateLevel6();
                break;
            case 7:
                generateLevel7();
                break;
        }
    }

    void resetLvl()
    {
        //Destroy(player);
        generateLevel(Loader.level);
    }

    void generateLevel1()
    {
        player.transform.position = new Vector3(1f, 1f, 1f);
        Tut1.SetActive(true);
    }

    void generateLevel2()
    {
        player.transform.position = new Vector3(1f, 1f, 1f);
        instantiateEnemy("Guard", 5f, 1f, 90f);
        Tut2.SetActive(true);
    }

    void generateLevel3()
    {
        player.transform.position = new Vector3(3f, 1f, 1f);
        instantiateEnemy("Guard", 3f, 7f, 180f);
    }

    void generateLevel4()
    {
        player.transform.position = new Vector3(1f, 1f, 1f);

        instantiateEnemy("Guard", 1f, 7f, 90f);
        instantiateEnemy("Guard", 3f, 3f, 180f);
        instantiateEnemy("Guard", 3f, 5f, 180f);
        instantiateEnemy("Guard", 5f, 3f, -90f);
        instantiateEnemy("Guard", 5f, 5f, -90f);
        instantiateEnemy("Guard", 7f, 1f, 0f);
        instantiateEnemy("Guard", 7f, 5f, 180f);
    }

    void generateLevel6()
    {
        player.transform.position = new Vector3(5f, 1f, 1f);

        instantiateEnemy("Patrol", 3f, 3f, -90f);
        instantiateEnemy("Patrol", 3f, 5f, -90f);
        instantiateEnemy("Patrol", 7f, 7f, 90f);

        instantiateCollectible(new Vector3(1f, 0.8f, 1f));
    }

    void generateLevel5()
    {
        player.transform.position = new Vector3(5f, 1f, 1f);
        instantiateEnemy("Patrol", 1f, 3f, 90f);
        instantiateCollectible(new Vector3(1f, 0.8f, 5f));
    }

    void generateLevel7()
    {
        player.transform.position = new Vector3(1f, 1f, 1f);
        instantiateEnemy("Patrol", 7f, 3f, 90f);
        instantiateEnemy("Patrol", 5f, 5f, -90f);
        instantiateCollectible(new Vector3(7f, 0.8f, 7f));
    }

    void instantiateEnemy(string type, float x, float z, float angleY)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(type), new Vector3(x, 1f, z), Quaternion.identity);
        go.transform.Rotate(0f, angleY, 0f);
        Enemy enemy = go.GetComponent(type) as Enemy;
        enemies.Add(enemy);
    }

    void instantiateCollectible(Vector3 position)
    {
        collectible = (GameObject)Instantiate(Resources.Load("Collectible"), position, Quaternion.identity);
        collectibleExists = true;
    }
}
