using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public TileMaster tileMaster;
    public CardMaster cardMaster;
    public GridCursor cursor;
    private ScriptableObjects.Cards.Card activeCard;

    public List<Hero> heroes;
    
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
    public void OnCardClicked(UICard uiCard){
        activeCard = uiCard.card;
        cursor.ChangeVisibility(true);
    }

    private void ResetCards(){
        activeCard = null;
        cursor.ChangeVisibility(false);
        cardMaster.ResetCards();
    }

    public void EndTurn()
    {
        ResetCards();
        foreach (var hero in heroes)
        {
            hero.OnEndTurn();
        }
    }
}
