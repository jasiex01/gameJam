using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new TileData", menuName = "Tile Data", order = 0)]
    public class TileGameData : ScriptableObject
    {
        public List<Tile> tiles;

        public bool passable;
        public float movementCost;
    }
}