using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects.Cards
{
    public abstract class Card : ScriptableObject
    {
        public Sprite card;
        public string description;
    }

    [CreateAssetMenu(fileName = "new MonsterCard", menuName = "Cards/Monster Card", order = 0)]
    public class MonsterCard : Card
    {
        public int strength;
        public Hero.Class weakAgainst;
        public Hero.Class strongAgainst;
    }

    [CreateAssetMenu(fileName = "new LureCard", menuName = "Cards/Lure Card", order = 0)]
    public class LureCard : Card
    {
        public Sprite lure;
        public List<Hero.Class> effectiveFor;
    }
    
    [CreateAssetMenu(fileName = "new TrapCard", menuName = "Cards/Trap Card", order = 0)]
    public class TrapCard : Card
    {
        public Sprite trap;
        public int strength;
        public Hero.Class counteredBy;
    }

    [CreateAssetMenu(fileName = "new ObstacleCard", menuName = "Cards/Obstacle Card", order = 0)]
    public class ObstacleCard : Card
    {
        public Tile tile;
    }
}