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
}