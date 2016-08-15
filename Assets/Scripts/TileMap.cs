using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

	public Player player;
	public int[,] tiles;
    public TileType[] tileTypes;

	const int WALKABLE = 0;
	const int NONWALKABLE = 1;

    int mapSizeX = 10;
    int mapSizeZ = 10;

	private Transform tileMap;
	private List <Vector3> tilePositions = new List<Vector3> ();

    // Use this for initialization
    void generate()
    {
        generateMapData();
        generateMapVisual();
    }

    void generateMapData()
    {
        GameObject walkable = (GameObject)Instantiate(Resources.Load("Walkable"));
        GameObject nonWalkable = (GameObject)Instantiate(Resources.Load("NonWalkable"));

        tileTypes = new TileType[2];
        tileTypes[0] = new TileType(walkable, true);
        tileTypes[1] = new TileType(nonWalkable, false);

        tiles = new int[mapSizeX, mapSizeZ];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
				tilePositions.Add(new Vector3(x, 0f, z));
                tiles[x, z] = 0;
            }
        }

        tiles[4, 4] = 1;
        tiles[4, 6] = 1;
        tiles[4, 8] = 1;
    }

    void generateMapVisual()
    {
		tileMap = new GameObject ("TileMap").transform;

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
				TileType tt = tileTypes [tiles [x, z]];
				GameObject go = (GameObject) Instantiate(tt.tileVisualPrefab, 
															new Vector3(x, 0f, z), 
															Quaternion.identity);

				go.transform.SetParent (tileMap);
            }
        }
    }

	// Update is called once per frame
	void Update () {
	}
}
