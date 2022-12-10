using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMaster : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileGameData> tileGameDataList;
    
    Dictionary<Tile, TileGameData> tileGameDataDictionary = new ();
    
    Dictionary<Vector3Int, Pathfinding.Node> pathfindingNodeDictionary;
    public Pathfinding.Node GetPathfindingNode(Vector3Int position) => pathfindingNodeDictionary[position];

    private void Awake()
    {
        foreach (var tileData in tileGameDataList)
        {
            foreach (var tile in tileData.tiles)
            {
                tileGameDataDictionary.Add(tile, tileData);
            }
        }

        var nodes = Pathfinding.GetPathfindingNodes(this);
        pathfindingNodeDictionary = new Dictionary<Vector3Int, Pathfinding.Node>();
        foreach (var node in nodes)
        {
            pathfindingNodeDictionary.Add(new Vector3Int(node.x, node.y, 0), node);
        }
    }
    
    public TileGameData GetTileGameData(Tile tile)
    {
        return tileGameDataDictionary[tile];
    }
    
    public float GetTileCost(Tile tile)
    {
        if (tile == null)
        {
            return float.PositiveInfinity;
        }
        
        var tileGameData = GetTileGameData(tile);
        return tileGameData.Cost;
    }
}