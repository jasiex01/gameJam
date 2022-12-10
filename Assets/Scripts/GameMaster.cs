using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public TileMaster tileMaster;
    public CardMaster cardMaster;
    public GridCursor cursor;
    private UICard chosenCard;
    
    void Start()
    {
        ResetCards();
    }
    
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            ResetCards();
        }
    }
    public void onCardClicked(UICard card){
        chosenCard = card;
        cursor.ChangeVisibility(true);
    }

    private void ResetCards(){
        chosenCard = null;
        cursor.ChangeVisibility(false);
        cardMaster.ResetCards();
    }
}
