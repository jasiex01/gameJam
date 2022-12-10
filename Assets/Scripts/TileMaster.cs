using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class TileMaster : MonoBehaviour
    {
        public Tilemap tilemap;
        public List<TileGameData> tileGameDataList;
        
        public Dictionary<Tile, TileGameData> tileGameDataDictionary = new ();

        private void Awake()
        {
            foreach (var tileData in tileGameDataList)
            {
                foreach (var tile in tileData.tiles)
                {
                    tileGameDataDictionary.Add(tile, tileData);
                }
            }
        }
        
        public TileGameData GetTileGameData(Tile tile)
        {
            return tileGameDataDictionary[tile];
        }
    }
}