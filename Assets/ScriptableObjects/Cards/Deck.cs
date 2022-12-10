using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "new Deck", menuName = "Cards/Deck", order = 0)]
    public class Deck : ScriptableObject
    {
        public List<Card> deck;
    }
}


