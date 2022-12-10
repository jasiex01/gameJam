using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Pathfinding
{
    
    public class Node
    {
        public int x;
        public int y;
        public float entranceCost;
        public HashSet<Node> neighbors;
        
        public Vector3Int ToVector3Int() => new Vector3Int(x, y, 0);
    }

    public static List<Node> GetPathfindingNodes(TileMaster tileMaster)
    {
        var bounds = tileMaster.tilemap.cellBounds;
        var nodes = new List<Node>();
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                var tile = tileMaster.tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    var node = new Node();
                    node.x = x;
                    node.y = y;
                    node.entranceCost = tileMaster.GetTileCost(tile);
                    node.neighbors = new HashSet<Node>();
                    nodes.Add(node);
                }
            }
        }
        
        foreach (var node in nodes)
        {
            List<Node> nullishNeighbors = new()
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
    
    static List<Node> ReconstructPath(IReadOnlyDictionary<Node, Node> cameFrom, Node current)
    {
        var totalPath = new List<Node>{current};
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);    
        }
        totalPath.Reverse();
        return totalPath;
    }
    
    public static List<Node> FindPath(Node start, Node end)
    {
        float H(Node node) => Vector2.Distance(new Vector2(node.x, node.y), new Vector2(end.x, end.y));
        
        var gScore = new Utils.DefaultDictionary<Node, float>(float.PositiveInfinity);
        gScore[start] = 0;
        
        var fScore = new Utils.DefaultDictionary<Node, float>(float.PositiveInfinity);
        fScore[start] = H(start);
        
        var openSet = new SortedSet<Node>(Comparer<Node>.Create(
            (a, b) => fScore[a].CompareTo(fScore[b]))) {start};
        
        var cameFrom = new Dictionary<Node, Node>();

        while (openSet.Count > 0)
        {
            var current = openSet.Min;
            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
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