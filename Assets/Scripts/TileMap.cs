using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    public int number;

	public Player player;
	public static TileType[,] tiles;
    public TileType[] tileTypes;

	const int WALKABLE = 0;
	const int NONWALKABLE = 1;

    int mapSizeX = 10;
    int mapSizeZ = 10;

	private Transform tileMap;
	private List <Vector3> tilePositions = new List<Vector3> ();

    public void Start()
    {
        generateMapData();
        generateMapVisual();
    }
    
    void generateMapData()
    {
        GameObject walkable = (GameObject)Instantiate(Resources.Load("Walkable"));
        GameObject nonWalkable = (GameObject)Instantiate(Resources.Load("NonWalkable"));

        tileTypes = new TileType[2];
        tileTypes[WALKABLE] = new TileType(walkable, true);
        tileTypes[NONWALKABLE] = new TileType(nonWalkable, false);

        tiles = new TileType[mapSizeX, mapSizeZ];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
				tilePositions.Add(new Vector3(x, 0f, z));
                tiles[x, z] = tileTypes[WALKABLE];
            }
        }

        tiles[4, 4] = tileTypes[NONWALKABLE];
        tiles[4, 6] = tileTypes[NONWALKABLE];
        tiles[4, 8] = tileTypes[NONWALKABLE];
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

	// Update is called once per frame
	void Update () {
	}
}
