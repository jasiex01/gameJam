using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ScriptableObjects.Cards;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CardInfo : MonoBehaviour
{
    public static CardInfo Instance { get; private set; }
    
    private TextMeshProUGUI text;

    public string placeholder;
    private void Awake()
    {
        Instance = this;
        text = GetComponent<TextMeshProUGUI>();
        text.text = placeholder;
    }

    public void UpdateInfo([CanBeNull] Card card)
    {
        text.text = card != null ? GetDescription(card) : placeholder;
    }

    public static string GetDescription(Card card)
    {
        switch (card)
        {
            case MonsterCard monsterCard:
                return
                    $"{card.name.ToUpper()}: Zadaje {monsterCard.strength} obrażeń, lub {monsterCard.strength + 1} jeżeli zaatakuje {GetHeroClassDisplayName(monsterCard.strongAgainst)}. " +
                    $"\nNie zada obrażeń, jeżeli zaatakuje {GetHeroClassDisplayName(monsterCard.weakAgainst)}";
            case LureCard lureCard:
                return
                    $"{card.name.ToUpper()}: Przyciąga {string.Join(" i ", lureCard.effectiveFor.Select(GetHeroClassDisplayName))} do siebie." +
                    "\nMusisz położyć przynęte na polu obok przeciwnika.";
            default:
                throw new NotSupportedException();
        }
    }
    
    public static string GetHeroClassDisplayName(Hero.Class @class)
    {
        switch (@class)
        {
            case Hero.Class.Cleric:
                return "KLERYK";
            case Hero.Class.Mage:
                return "MAG";
            case Hero.Class.Scout:
                return "ZWIADOWCA";
            case Hero.Class.Warrior:
                return "WOJOWNIK";
            default:
                throw new ArgumentOutOfRangeException(nameof(@class), @class, null);
        }
    }
}
