using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMaster : MonoBehaviour
{
    public static TileMaster Instance { get; private set; }
    
    public Tilemap tilemap;
    public List<TileGameData> tileGameDataList;
    
    Dictionary<Tile, TileGameData> tileGameDataDictionary = new ();
    
    Dictionary<Vector3Int, Pathfinding.Node> pathfindingNodeDictionary = new ();
    public Pathfinding.Node GetPathfindingNode(Vector3Int position) => pathfindingNodeDictionary[position];

    public void RebuildPathfindingNodes()
    {
        var nodes = Pathfinding.GetPathfindingNodes(this);
        pathfindingNodeDictionary.Clear();
        foreach (var node in nodes)
        {
            pathfindingNodeDictionary.Add(new Vector3Int(node.x, node.y, 0), node);
        }
    }
    
    private void Awake()
    {
        Instance = this;
        foreach (var tileData in tileGameDataList)
        {
            foreach (var tile in tileData.tiles)
            {
                tileGameDataDictionary.Add(tile, tileData);
            }
        }
        
        RebuildPathfindingNodes();
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