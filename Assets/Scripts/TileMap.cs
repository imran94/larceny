using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    public int number;

	public GameObject player;
	public static TileType[,] tiles;
    public TileType[] tileTypes;

	const int WALKABLE = 0;
	const int NONWALKABLE = 1;
	const int EXIT = 2;
    const int PATH_HORIZONTAL = 3;
    const int PATH_VERTICAL = 4;

    int mapSizeX;
    int mapSizeZ;

	private Transform tileMap;
	private List <Vector3> tilePositions = new List<Vector3> ();

    public void Start()
    {
    }
    
    void generateTileData()
    {
        GameObject walkable = (GameObject)Instantiate (Resources.Load ("Walkable"));
        GameObject nonWalkable = (GameObject)Instantiate (Resources.Load ("NonWalkable"));
		GameObject exit = (GameObject)Instantiate (Resources.Load ("Exit"));
        GameObject pathHorizontal = (GameObject)Instantiate(Resources.Load("Path"));
        GameObject pathVertical = (GameObject)Instantiate(Resources.Load("Path"));
        pathVertical.transform.Rotate(new Vector3(0f, 90f, 0f));

        tileTypes = new TileType[5];
        tileTypes [WALKABLE] = new TileType (walkable, true);
        tileTypes [NONWALKABLE] = new TileType (nonWalkable, false);
		tileTypes [EXIT] = new TileType (exit, true);
        tileTypes [PATH_HORIZONTAL] = new TileType (pathHorizontal, true);
        tileTypes [PATH_VERTICAL] = new TileType (pathVertical, true);
    }

    void generateMapVisual()
    {
		tileMap = new GameObject ("TileMap").transform;

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
				TileType tt = tiles [x, z];
				GameObject go = (GameObject) Instantiate(tt.tileVisualPrefab, 
															new Vector3(x, 0f, z), 
															Quaternion.identity);
				
				go.transform.SetParent (tileMap);
            }
        }
    }

    void genericLevel()
    {
        tiles = new TileType[mapSizeX, mapSizeZ];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                tilePositions.Add(new Vector3(x, 0f, z));
                tiles[x, z] = tileTypes[NONWALKABLE];
            }
        }
    }

    public void generateLevel(int level)
    {
        generateTileData();

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

        generateMapVisual();
    }

    void assignTiles(int mapSizeX, int mapSizeZ, int[,] lvlArray)
    {
        for (int i = 0; i < mapSizeZ; i++)
        {
            for (int j = 0; j < mapSizeX; j++)
            {
                tiles[j, i] = tileTypes[lvlArray[i, j]];
            }
        }
    }

    void generateLevel1()
    {
        mapSizeX = 7;
        mapSizeZ = 3;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 3, 2, 1},
            {1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);

        //for (int i = 0; i < mapSizeZ; i++)
        //{
        //    for (int j = 0; j < mapSizeX; j++)
        //    {
        //        tiles[j, i] = tileTypes[lvlArray[i, j]];
        //    }
        //}

        //tiles[5, 1] = tileTypes[EXIT];
    }

    void generateLevel2()
    {
        mapSizeX = 9;
        mapSizeZ = 3;

        genericLevel();

        for (int i = 1; i < 7; i++)
            tiles[i, 1] = tileTypes[WALKABLE];

        tiles[7, 1] = tileTypes[EXIT];
    }

    void generateLevel3()
    {
        mapSizeX = 6;
        mapSizeZ = 9;

        genericLevel();
        
        for (int i = 3; i < 8; i++)
            tiles[1, i] = tileTypes[WALKABLE];

        tiles[2, 3] = tileTypes[WALKABLE];
        tiles[2, 5] = tileTypes[WALKABLE];
        tiles[2, 7] = tileTypes[WALKABLE];

        for (int i = 1; i < 8; i++)
            tiles[3, i] = tileTypes[WALKABLE];

        tiles[4, 5] = tileTypes[WALKABLE];
        tiles[5, 5] = tileTypes[EXIT];
    }

    void generateLevel4()
    {
        mapSizeX = mapSizeZ = 12;
        genericLevel();

        tiles[1, 1] = tileTypes[WALKABLE];
        tiles[2, 1] = tileTypes[WALKABLE];
        tiles[3, 1] = tileTypes[WALKABLE];
        tiles[4, 1] = tileTypes[WALKABLE];
        tiles[5, 1] = tileTypes[WALKABLE];
        tiles[6, 1] = tileTypes[WALKABLE];
        tiles[7, 1] = tileTypes[WALKABLE];

        tiles[1, 1] = tileTypes[WALKABLE];
        tiles[1, 2] = tileTypes[WALKABLE];
        tiles[1, 3] = tileTypes[WALKABLE];
        tiles[1, 4] = tileTypes[WALKABLE];
        tiles[1, 5] = tileTypes[WALKABLE];
        tiles[1, 6] = tileTypes[WALKABLE];
        tiles[1, 7] = tileTypes[WALKABLE];

        tiles[2, 3] = tileTypes[WALKABLE];
        tiles[3, 3] = tileTypes[WALKABLE];
        tiles[4, 3] = tileTypes[WALKABLE];
        tiles[5, 3] = tileTypes[WALKABLE];
        tiles[7, 3] = tileTypes[WALKABLE];

        tiles[2, 5] = tileTypes[WALKABLE];
        tiles[3, 5] = tileTypes[WALKABLE];
        tiles[4, 5] = tileTypes[WALKABLE];
        tiles[5, 5] = tileTypes[WALKABLE];
        tiles[6, 5] = tileTypes[WALKABLE];
        tiles[7, 5] = tileTypes[WALKABLE];

        tiles[2, 7] = tileTypes[WALKABLE];
        tiles[3, 7] = tileTypes[WALKABLE];
        tiles[4, 7] = tileTypes[WALKABLE];
        tiles[5, 7] = tileTypes[WALKABLE];
        tiles[6, 7] = tileTypes[WALKABLE];
        tiles[7, 7] = tileTypes[WALKABLE];

        tiles[8, 3] = tileTypes[WALKABLE];
        tiles[9, 3] = tileTypes[EXIT];
    }

    void generateLevel5()
    {
        mapSizeX = 11;
        mapSizeZ = 10;

        genericLevel();

        tiles[5, 1] = tileTypes[WALKABLE];
        tiles[5, 2] = tileTypes[WALKABLE];

        for (int i = 1; i < 10; i++)
        {
            tiles[i, 3] = tileTypes[WALKABLE];
            tiles[i, 7] = tileTypes[WALKABLE];
        }

        for (int i = 4; i < 7; i++)
        {
            tiles[1, i] = tileTypes[WALKABLE];
            tiles[9, i] = tileTypes[WALKABLE];
        }

        for (int i = 2; i < 8; i++)
            tiles[i, 5] = tileTypes[WALKABLE];

        tiles[5, 8] = tileTypes[WALKABLE];
        tiles[5, 9] = tileTypes[EXIT];
    }

    // Update is called once per frame
    void Update () {
	}
}
