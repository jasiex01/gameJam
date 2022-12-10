using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Pathfinding
{
    
    public class PathfindingNode
    {
        public int x;
        public int y;
        public float entranceCost;
        public HashSet<PathfindingNode> neighbors;
    }

    public static List<PathfindingNode> GetPathfindingNodes(TileMaster tileMaster)
    {
        var bounds = tileMaster.tilemap.cellBounds;
        var nodes = new List<PathfindingNode>();
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                var tile = tileMaster.tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    var node = new PathfindingNode();
                    node.x = x;
                    node.y = y;
                    node.entranceCost = tileMaster.GetTileCost(tile);
                    node.neighbors = new HashSet<PathfindingNode>();
                    nodes.Add(node);
                }
            }
        }
        
        foreach (var node in nodes)
        {
            List<PathfindingNode> nullishNeighbors = new()
            {
                nodes.Find(n => n.x == node.x - 1 && n.y == node.y),
                nodes.Find(n => n.x == node.x + 1 && n.y == node.y),
                nodes.Find(n => n.x == node.x && n.y == node.y - 1),
                nodes.Find(n => n.x == node.x && n.y == node.y + 1),
            };
            foreach (var neighbor in nullishNeighbors)
            {
                if (neighbor != null)
                {
                    node.neighbors.Add(neighbor);
                }
            }
        }
        
        return nodes;
    }
    
    static List<PathfindingNode> ReconstructPath(IReadOnlyDictionary<PathfindingNode, PathfindingNode> cameFrom, PathfindingNode current)
    {
        var totalPath = new List<PathfindingNode>{current};
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);    
        }
        totalPath.Reverse();
        return totalPath;
    }
    
    public static List<PathfindingNode> FindPath(PathfindingNode start, PathfindingNode end)
    {
        float H(PathfindingNode node) => Vector2.Distance(new Vector2(node.x, node.y), new Vector2(end.x, end.y));
        
        var gScore = new Utils.DefaultDictionary<PathfindingNode, float>(float.PositiveInfinity);
        gScore[start] = 0;
        
        var fScore = new Utils.DefaultDictionary<PathfindingNode, float>(float.PositiveInfinity);
        fScore[start] = H(start);
        
        var openSet = new SortedSet<PathfindingNode>(Comparer<PathfindingNode>.Create(
            (a, b) => fScore[a].CompareTo(fScore[b]))) {start};
        
        var cameFrom = new Dictionary<PathfindingNode, PathfindingNode>();

        Debug.Log($"beginning pathfinding, openSet has {openSet.Count} items");
        while (openSet.Count > 0)
        {
            var current = openSet.Min;
            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            Debug.Log($"Node {current.x}-{current.y} has {current.neighbors.Count} neighbors");
            foreach (var neighbor in current.neighbors)
            {
                var tentativeGScore = gScore[current] + neighbor.entranceCost;
                if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + H(neighbor);
                openSet.Add(neighbor);
            }
        }
        return null;
    }
}