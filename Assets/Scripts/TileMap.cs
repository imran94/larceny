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
        GameObject pathVertical = (GameObject)Instantiate(Resources.Load("PathVert"));

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
            case 6:
                generateLevel6();
                break;
            case 7:
                generateLevel7();
                break;
        }

        generateMapVisual();
    }

    void assignTiles(int mapSizeX, int mapSizeZ, int[,] lvlArray)
    {
        Debug.Log("assigning tiles");
        for (int i = 0; i < mapSizeZ; i++)
        {
            Debug.Log(i);
            for (int j = 0; j < mapSizeX; j++)
            {
                tiles[j, i] = tileTypes[lvlArray[mapSizeZ - 1 - i, j]];
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
    }

    void generateLevel2()
    {
        mapSizeX = 9;
        mapSizeZ = 3;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 2, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);
    }

    void generateLevel3()
    {
        mapSizeX = 6;
        mapSizeZ = 9;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 1, 1},
            {1, 4, 1, 4, 1, 1},
            {1, 0, 1, 0, 3, 2},
            {1, 4, 1, 4, 1, 1},
            {1, 0, 3, 0, 1, 1},
            {1, 1, 1, 4, 1, 1},
            {1, 1, 1, 0, 1, 1},
            {1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);
    }

    void generateLevel4()
    {
        mapSizeX = 11; mapSizeZ = 9;
        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 1, 1, 1},
            {1, 4, 1, 4, 1, 1, 1, 4, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 1, 1, 1},
            {1, 4, 1, 4, 1, 4, 1, 4, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 1, 0, 3, 2, 1},
            {1, 4, 1, 4, 1, 1, 1, 4, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);
    }

    void generateLevel5()
    {
        mapSizeX = 11;
        mapSizeZ = 10;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 3, 0, 1},
            {1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 1, 0, 1},
            {1, 4, 1, 4, 1, 1, 1, 1, 1, 4, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 3, 0, 1},
            {1, 4, 1, 1, 1, 4, 1, 1, 1, 1, 1},
            {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);
    }

    void generateLevel6()
    {
        mapSizeX = 7;
        mapSizeZ = 7;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 1, 2, 1},
            {1, 4, 1, 4, 1, 4, 1},
            {1, 0, 3, 0, 3, 0, 1},
            {1, 4, 1, 4, 1, 4, 1},
            {1, 0, 3, 0, 3, 0, 1},
            {1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);
    }

    void generateLevel7()
    {
        mapSizeX = 11;
        mapSizeZ = 9;

        genericLevel();

        int[,] lvlArray = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 0, 3, 0, 3, 0, 1, 1, 1},
            {1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 3, 2, 1},
            {1, 1, 1, 4, 1, 4, 1, 4, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 3, 0, 1},
            {1, 1, 1, 4, 1, 4, 1, 4, 1, 1, 1},
            {1, 0, 3, 0, 3, 0, 3, 0, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        assignTiles(mapSizeX, mapSizeZ, lvlArray);

    }

    // Update is called once per frame
    void Update () {
	}
}
