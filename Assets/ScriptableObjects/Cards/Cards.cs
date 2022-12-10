using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cards
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public abstract class BaseCard : ScriptableObject
    {
        public Sprite card;
        public string description;
    }

    [CreateAssetMenu(fileName = "new MonsterCard", menuName = "Cards/Monster Card", order = 0)]
    public class MonsterCard : BaseCard
    {
        public int strength;
        public Hero.Class weakAgainst;
        public Hero.Class strongAgainst;
    }

    [CreateAssetMenu(fileName = "new LureCard", menuName = "Cards/Monster Card", order = 0)]
    public class LureCard : BaseCard
    {
        public Sprite lure;
        public List<Hero.Class> effectiveFor;
    }
    
    [CreateAssetMenu(fileName = "new TrapCard", menuName = "Cards/Monster Card", order = 0)]
    public class TrapCard : BaseCard
    {
        public Sprite trap;
        public int strength;
        public Hero.Class counteredBy;
    }

    [CreateAssetMenu(fileName = "new ObstacleCard", menuName = "Cards/Obstacle Card", order = 0)]
    public class ObstacleCard : BaseCard
    {
        public Tile tile;
    }
}