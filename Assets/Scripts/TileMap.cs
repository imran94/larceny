using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

    public TileType[] tileTypes;

    int[,] tiles;

    int mapSizeX = 10;
    int mapSizeZ = 10;

    // Use this for initialization
    void Awake()
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
                tiles[x, z] = 0;
            }
        }

        tiles[4, 4] = 1;
        tiles[4, 6] = 1;
        tiles[4, 8] = 1;
    }

    void generateMapVisual()
    {
        Debug.Log(tileTypes.Length);

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                TileType tt = tileTypes[tiles[x, z]];
                Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
