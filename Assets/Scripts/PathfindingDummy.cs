using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class PathfindingDummy : MonoBehaviour
    {
        public TileMaster tileMaster;
        public Hero hero;
        public Vector3Int target;

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                var nodes = Pathfinding.GetPathfindingNodes(tileMaster);
            
                print($"Map contains {nodes.Count} nodes. On average, each node has {nodes.Average(n => n.neighbors.Count):F2} neighbors");

                var startNode = nodes.Find(n => n.x == hero.currentCell.x && n.y == hero.currentCell.y);
                var targetNode = nodes.Find(n => n.x == target.x && n.y == target.y);
            
                var path = Pathfinding.FindPath(startNode, targetNode);
                if (path != null)
                {
                    Debug.Log($"{path.Count.ToString()}-node path found");
                    foreach (var node in path)
                    {
                        Gizmos.DrawSphere(tileMaster.tilemap.GetCellCenterWorld(new Vector3Int(node.x, node.y, 0)), 0.1f);
                    }
                }
                else
                {
                    Debug.Log("No path found");
                }
            }
        }
    }
}