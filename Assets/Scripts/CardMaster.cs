using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using DG.Tweening;

public class CardMaster : MonoBehaviour
{
    public RectTransform anchor;
    public UICard cardPrefab;
    private List<UICard> cardList = new ();
    public int initialCardCount;
    public float spacing;
    public DOTweenAnimationTemplate cardMoveAnimation;
    public DOTweenAnimationTemplate cardScaleAnimation;
    public GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ResetCards() {
        
        for (int i = 0; i < cardList.Count; i++){
            Destroy(cardList[i].gameObject);
        }

        cardList.Clear();
        // Instantiate cards
        for (int i = 0; i < initialCardCount; i++){
            var newCard = Instantiate(cardPrefab, anchor);
            newCard.cardMaster = this;
            newCard.transform.localPosition = new Vector3(0,-300,0);
            cardList.Add(newCard);
        }
        
        UpdateCards();
    }

    private void UpdateCards(){
        float offset = (-1 * (cardList.Count - 1) * spacing) / 2;
        for (int i = 0; i < cardList.Count; i++){
            var position = new Vector3(offset + (i * spacing), 0, 0);
            cardList[i].transform.DOLocalMove(position, cardMoveAnimation.duration).SetEase(cardMoveAnimation.easeType);
        }
    }


    // Update is called once per frame
    void Update()
    {        
    }

    public void OnCardClicked(UICard card){
        card.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), cardScaleAnimation.duration);

        gameMaster.onCardClicked(card);
        
        for (int i = 0; i < initialCardCount; i++){
            if(cardList[i] != card)
                cardList[i].transform.DOScale(new Vector3(1,1,1), cardScaleAnimation.duration);
        }
    }
}
