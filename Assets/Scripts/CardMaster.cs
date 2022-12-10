using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CardMaster : MonoBehaviour
{
    public RectTransform anchor;
    public UICard cardPrefab;
    private List<UICard> cardList = new ();
    public int cardCount;
    public float spacing;
    public DOTweenAnimationTemplate cardMoveAnimation;
    public DOTweenAnimationTemplate cardScaleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        ResetCards();
    }

    private void ResetCards() {
        for (int i = 0; i < cardList.Count; i++){
            Destroy(cardList[i].gameObject);
        }

        cardList.Clear();

        for (int i = 0; i < cardCount; i++){
            var newCard = Instantiate(cardPrefab, anchor);
            newCard.cardMaster = this;
            newCard.transform.localPosition = new Vector3(0,-300,0);
            cardList.Add(newCard);
        }
        // Instantiate cards
        float offset = (-1 * cardCount * spacing) / 2;
        for (int i = 0; i < cardCount; i++){
            var position = new Vector3(offset + (i * spacing), 0, 0);
            cardList[i].transform.DOLocalMove(position, cardMoveAnimation.duration).SetEase(cardMoveAnimation.easeType);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            ResetCards();
        }
        
    }

    public void OnCardClicked(UICard card){
        card.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), cardScaleAnimation.duration);

        for (int i = 0; i < cardCount; i++){
            if(cardList[i] != card)
                cardList[i].transform.DOScale(new Vector3(1,1,1), cardScaleAnimation.duration);
        }
    }
}
