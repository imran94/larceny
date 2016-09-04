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

        tileTypes = new TileType[3];
        tileTypes [WALKABLE] = new TileType (walkable, true);
        tileTypes [NONWALKABLE] = new TileType (nonWalkable, false);
		tileTypes [EXIT] = new TileType (exit, true);
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

    void generateLevel1()
    {
        
    }

    void generateLevel2()
    {

    }

    void generateLevel3()
    {

    }

    void generateLevel4()
    {
        mapSizeX = mapSizeZ = 12;

        tiles = new TileType[mapSizeX, mapSizeZ];
        Debug.Log(tileTypes.Length);

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                tilePositions.Add(new Vector3(x, 0f, z));
                tiles[x, z] = tileTypes[NONWALKABLE];
            }
        }

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

    }
    
    // Update is called once per frame
    void Update () {
	}
}
