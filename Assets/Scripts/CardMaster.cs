using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ScriptableObjects;
using DG.Tweening;
using ScriptableObjects.Cards;

public class CardMaster : MonoBehaviour
{
    public static CardMaster Instance { get; private set; }
    
    public RectTransform anchor;
    public UICard cardPrefab;
    private List<UICard> uiCards = new ();
    private UICard activeUiCard;
    public int initialCardCount;
    public float spacing;
    public DOTweenAnimationTemplate cardMoveAnimation;
    public DOTweenAnimationTemplate cardScaleAnimation;

    public Deck deckTemplate;
    public List<Card> deck = new ();

    private void Awake()
    {
        Instance = this;
    }

    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            deck = new List<Card>(deckTemplate.deck);
            deck.Shuffle();
        }

        var card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    public void DrawNewHand()
    {
        for (int i = 0; i < uiCards.Count; i++)
        {
            uiCards[i].transform.DOKill();
            Destroy(uiCards[i].gameObject);
        }

        uiCards.Clear();
        
        for (int i = 0; i < initialCardCount; i++)
        {
            var card = DrawCard();
            var uiCard = Instantiate(cardPrefab, anchor);
            uiCard.cardMaster = this;
            uiCard.card = card;
            uiCard.transform.localPosition = new Vector3(0,-300,0);
            uiCards.Add(uiCard);
        }
        
        UpdateCards();
    }

    public void ResetCards() {
        
        for (int i = 0; i < uiCards.Count; i++)
        {
            uiCards[i].transform.DOKill();
            Destroy(uiCards[i].gameObject);
        }

        uiCards.Clear();
        
        for (int i = 0; i < initialCardCount; i++){
            var newCard = Instantiate(cardPrefab, anchor);
            newCard.cardMaster = this;
            newCard.transform.localPosition = new Vector3(0,-300,0);
            uiCards.Add(newCard);
        }
        
        UpdateCards();
    }

    private void UpdateCards(){
        float offset = (-1 * (uiCards.Count - 1) * spacing) / 2;
        for (int i = 0; i < uiCards.Count; i++){
            var position = new Vector3(offset + (i * spacing), 0, 0);
            uiCards[i].transform.DOLocalMove(position, cardMoveAnimation.duration).SetEase(cardMoveAnimation.easeType);
        }
    }

    public void OnCardClicked(UICard uiCard){
        uiCard.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), cardScaleAnimation.duration);

        activeUiCard = uiCard;
        GameMaster.Instance.OnCardClicked(uiCard);
        
        foreach (var c in uiCards)
        {
            if (c != uiCard)
            {
                c.transform.DOScale(new Vector3(0.8f,0.8f,0.8f), cardScaleAnimation.duration);
            }
        }
    }
    
    public void RemoveActiveCard()
    {
        uiCards.Remove(activeUiCard);
        activeUiCard.transform.DOKill();
        Destroy(activeUiCard.gameObject);
        activeUiCard = null;
        UpdateCards();
    }
}
