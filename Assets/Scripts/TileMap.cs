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

    int mapSizeX = 10;
    int mapSizeZ = 10;

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
        }

        generateMapVisual();
    }

    void generateLevel1()
    {

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

        //tiles[4, 4] = tileTypes[WALKABLE];
        //tiles[4, 5] = tileTypes[WALKABLE];
        //tiles[4, 6] = tileTypes[WALKABLE];
        //tiles[4, 7] = tileTypes[WALKABLE];
        //tiles[4, 8] = tileTypes[WALKABLE];

        //tiles[5, 8] = tileTypes[WALKABLE];
        //tiles[6, 8] = tileTypes[WALKABLE];
        //tiles[7, 8] = tileTypes[WALKABLE];

        //tiles[8, 8] = tileTypes[EXIT];

        tiles[0, 0] = tileTypes[WALKABLE];
        tiles[1, 0] = tileTypes[WALKABLE];
        tiles[2, 0] = tileTypes[WALKABLE];
        tiles[3, 0] = tileTypes[WALKABLE];
        tiles[4, 0] = tileTypes[WALKABLE];
        tiles[5, 0] = tileTypes[WALKABLE];
        tiles[6, 0] = tileTypes[WALKABLE];

        tiles[0, 1] = tileTypes[WALKABLE];
        tiles[0, 2] = tileTypes[WALKABLE];
        tiles[0, 3] = tileTypes[WALKABLE];
        tiles[0, 4] = tileTypes[WALKABLE];
        tiles[0, 5] = tileTypes[WALKABLE];
        tiles[0, 6] = tileTypes[WALKABLE];

        tiles[1, 2] = tileTypes[WALKABLE];
        tiles[1, 4] = tileTypes[WALKABLE];

        tiles[2, 1] = tileTypes[WALKABLE];
        tiles[2, 2] = tileTypes[WALKABLE];
        tiles[2, 3] = tileTypes[WALKABLE];
        tiles[2, 4] = tileTypes[WALKABLE];
        tiles[2, 5] = tileTypes[WALKABLE];
        tiles[2, 6] = tileTypes[WALKABLE];

        tiles[3, 2] = tileTypes[WALKABLE];
        tiles[3, 4] = tileTypes[WALKABLE];

        tiles[4, 2] = tileTypes[WALKABLE];
        tiles[4, 3] = tileTypes[WALKABLE];
        tiles[4, 4] = tileTypes[WALKABLE];
        tiles[4, 6] = tileTypes[WALKABLE];

        tiles[5, 4] = tileTypes[WALKABLE];

        tiles[6, 1] = tileTypes[WALKABLE];
        tiles[6, 2] = tileTypes[WALKABLE];
        tiles[6, 3] = tileTypes[WALKABLE];
        tiles[6, 4] = tileTypes[WALKABLE];
        tiles[6, 5] = tileTypes[WALKABLE];
        tiles[6, 6] = tileTypes[WALKABLE];

        tiles[1, 6] = tileTypes[WALKABLE];
        tiles[3, 6] = tileTypes[WALKABLE];
        tiles[5, 6] = tileTypes[WALKABLE];
        tiles[6, 6] = tileTypes[WALKABLE];

        tiles[7, 2] = tileTypes[WALKABLE];
        tiles[8, 2] = tileTypes[EXIT];
    }

    // Update is called once per frame
    void Update () {
	}
}
