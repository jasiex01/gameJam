using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "new MonsterCard", menuName = "Cards/Monster Card", order = 0)]
    public class MonsterCard : Card
    {
        public int strength;
        public Hero.Class weakAgainst;
        public Hero.Class strongAgainst;
    }
}
