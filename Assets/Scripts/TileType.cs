using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType {

    public string name;
    public GameObject tileVisualPrefab;

    public bool isWalkable;

    public TileType(GameObject prefab, bool walkable)
    {
        name = prefab.tag;
        tileVisualPrefab = prefab;
        isWalkable = walkable;
    }
}
