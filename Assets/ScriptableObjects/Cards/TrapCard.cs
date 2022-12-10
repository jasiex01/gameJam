using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "new LureCard", menuName = "Cards/Lure Card", order = 0)]
    public class LureCard : Card
    {
        public Sprite lure;
        public List<Hero.Class> effectiveFor;
    }
}
