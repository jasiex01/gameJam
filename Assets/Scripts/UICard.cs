using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour, IPointerClickHandler
{
    public CardMaster cardMaster;
    public Sprite sprite;
    
    public Cards.BaseCard cardData;
    
    private Image image;
    public void OnPointerClick(PointerEventData eventData)
    {
        cardMaster.OnCardClicked(this);
    }

    void Awake()
    {
        image = GetComponent<Image>();
    }
}
