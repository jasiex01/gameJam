using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Cards;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour
{
    public TileMaster tileMaster;
    public CardMaster cardMaster;
    public GridCursor cursor;
    private Card activeCard;

    public List<Hero> heroes;

    public bool IsCellAValidTarget(Vector3Int cell, Card card)
    {
        var tile = tileMaster.tilemap.GetTile<Tile>(cell);
        if (tile == null)
        {
            return false;
        }
        
        var tileGameData = tileMaster.GetTileGameData(tile);
        
        switch (card)
        {
            case MonsterCard:
                return heroes.Any(hero => hero.currentCell == cell);
            
            case LureCard:
                return tileGameData.passable && heroes.Any(hero =>
                {
                    var heroCell = hero.currentCell;
                    return heroCell == cell + Vector3Int.up ||
                           heroCell == cell + Vector3Int.down ||
                           heroCell == cell + Vector3Int.left ||
                           heroCell == cell + Vector3Int.right;
                });
            
            case TrapCard:
                return tileGameData.passable && heroes.All(hero => hero.currentCell != cell);;
            
            case ObstacleCard:
                throw new NotImplementedException();
            
            default:
                throw new NotSupportedException();
        }
    }
    
    void Start()
    {
        ResetCards();
    }
    
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            ResetCards();
        }
        
        if (activeCard != null)
        {
            cursor.ChangeVisibility(true);
            bool validTarget = IsCellAValidTarget(cursor.cell, activeCard);
            
            cursor.SetValid(validTarget);
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (IsCellAValidTarget(cursor.cell, activeCard))
                {
                    // activeCard.Play(cursor.cell);
                    cardMaster.RemoveActiveCard();
                    activeCard = null;
                }
            }
        }
        else
        {
            cursor.ChangeVisibility(false);
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
