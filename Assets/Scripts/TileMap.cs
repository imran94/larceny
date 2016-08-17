﻿using UnityEngine;
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
        generateMapData();
        generateMapVisual();
    }
    
    void generateMapData()
    {
        GameObject walkable = (GameObject)Instantiate (Resources.Load ("Walkable"));
        GameObject nonWalkable = (GameObject)Instantiate (Resources.Load ("NonWalkable"));
		GameObject exit = (GameObject)Instantiate (Resources.Load ("Exit"));

        player = GameObject.Find("Player");

        tileTypes = new TileType[3];
        tileTypes [WALKABLE] = new TileType (walkable, true);
        tileTypes [NONWALKABLE] = new TileType (nonWalkable, false);
		tileTypes [EXIT] = new TileType (exit, true);

		tiles = new TileType[mapSizeX, mapSizeZ];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
				tilePositions.Add(new Vector3(x, 0f, z));
                tiles[x, z] = tileTypes[NONWALKABLE];
            }
        }

		// Convert to a function
		tiles [4, 4] = tileTypes [WALKABLE];
		tiles [4, 5] = tileTypes [WALKABLE];
        tiles [4, 6] = tileTypes [WALKABLE];
		tiles [4, 7] = tileTypes [WALKABLE];
		tiles [4, 8] = tileTypes [WALKABLE];

		tiles [5, 8] = tileTypes [WALKABLE];
		tiles [6, 8] = tileTypes [WALKABLE];
		tiles [7, 8] = tileTypes [WALKABLE];

		tiles [8, 8] = tileTypes [EXIT];

        player.transform.position = new Vector3(4f, 3f, 4f);
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
