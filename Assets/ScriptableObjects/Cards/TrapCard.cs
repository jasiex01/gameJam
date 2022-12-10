using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "new TrapCard", menuName = "Cards/Trap Card", order = 0)]
    public class TrapCard : Card
    {
        public Sprite trap;
        public int strength;
        public Hero.Class counteredBy;
    }
}
