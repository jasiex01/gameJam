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
