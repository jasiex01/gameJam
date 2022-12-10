using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using DG.Tweening;

public class CardMaster : MonoBehaviour
{
    public RectTransform anchor;
    public UICard cardPrefab;
    private List<UICard> uiCards = new ();
    private UICard activeUiCard;
    public int initialCardCount;
    public float spacing;
    public DOTweenAnimationTemplate cardMoveAnimation;
    public DOTweenAnimationTemplate cardScaleAnimation;
    public GameMaster gameMaster;

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
        gameMaster.OnCardClicked(uiCard);
        
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
