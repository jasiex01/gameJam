using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "new ObstacleCard", menuName = "Cards/Obstacle Card", order = 0)]
    public class ObstacleCard : Card
    {
        public Tile tile;
    }
}
