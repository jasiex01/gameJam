using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Cards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    
    public GridCursor cursor;
    private Card activeCard;

    public List<Hero> heroes;

    public List<Vector3Int> entryPoints;
    public Vector3Int exitPoint;
    
    public AudioSource audioSource;
    public AudioClip music;

    public bool IsCellAValidTarget(Vector3Int cell, Card card)
    {
        var tile = TileMaster.Instance.tilemap.GetTile<Tile>(cell);
        if (tile == null)
        {
            return false;
        }

        var tileGameData = TileMaster.Instance.GetTileGameData(tile);

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
                return tileGameData.passable && heroes.All(hero => hero.currentCell != cell);
                ;

            case ObstacleCard:
                throw new NotImplementedException();

            default:
                throw new NotSupportedException();
        }
    }

    void UseCard(Card card, Vector3Int cell)
    {
        if (!IsCellAValidTarget(cell, card)) return;
        
switch (card)
        {
            case MonsterCard monsterCard:
                var hitHeroes = heroes.Where(h => h.currentCell == cell);

                if (hitHeroes.Any(hero => hero.@class == monsterCard.weakAgainst))
                {
                    return;
                }

                int damage = monsterCard.strength +
                             (hitHeroes.Any(hero => hero.@class == monsterCard.strongAgainst) ? 1 : 0);

                print($"Will deal {damage} damage to {hitHeroes.Count()} heroes");
                for (int i = 0; i < damage; i++)
                {
                    if (!hitHeroes.Any())
                    {
                        return;
                    }
                    var maxStrength = hitHeroes.Where(hero => hero != null).Max(hero => hero.currentStrength);
                    hitHeroes.First(hero => hero.currentStrength == maxStrength).TakeDamage(1);
                }
                break;
            
            case LureCard lureCard:
                var affectedHeroes = heroes.Where(hero =>
                    lureCard.effectiveFor.Contains(hero.@class) && (hero.currentCell == cell + Vector3Int.up ||
                                                                    hero.currentCell == cell + Vector3Int.down ||
                                                                    hero.currentCell == cell + Vector3Int.left ||
                                                                    hero.currentCell == cell + Vector3Int.right));
                foreach (var hero in affectedHeroes)
                {
                    hero.Move(cell);
                }

                return;
            
            case TrapCard trapCard:
                throw new NotImplementedException();
            
            case ObstacleCard obstacleCard:
                throw new NotImplementedException();
            
            default:
                throw new NotSupportedException();
        }
    }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        CardMaster.Instance.DrawNewHand();
        
        // var startingCell = entryPoints[Random.Range(0, entryPoints.Count)];
        foreach (var hero in heroes)
        {
            hero.goal = exitPoint;
            hero.Move(hero.currentCell);
        }
        
        audioSource.PlayOneShot(music);
    }
    
    void Update()
    {
        if (activeCard != null)
        {
            cursor.ChangeVisibility(true);
            bool validTarget = IsCellAValidTarget(cursor.cell, activeCard);
            
            cursor.SetValid(validTarget);
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (IsCellAValidTarget(cursor.cell, activeCard))
                {
                    UseCard(activeCard, cursor.cell);
                    CardMaster.Instance.RemoveActiveCard();
                    CardMaster.Instance.cardsPlayedThisTurn += 1;
                    if (CardMaster.Instance.cardsPlayedThisTurn >= CardMaster.Instance.cardsToPlayInOneTurn)
                    {
                        CardMaster.Instance.ClearHand();
                    }
                    CardInfo.Instance.UpdateInfo(null);
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
        CardInfo.Instance.UpdateInfo(activeCard);
        cursor.ChangeVisibility(true);
    }

    public void EndTurn()
    {
        CardMaster.Instance.cardsPlayedThisTurn = 0;
        CardMaster.Instance.DrawNewHand();
        activeCard = null;
        cursor.ChangeVisibility(false);
        foreach (var hero in heroes)
        {
            hero.OnEndTurn();
        }
    }

    public void CheckForWin(){
        if(heroes.Count == 0){
            SceneManager.LoadScene("Win");
        }
    }
    
    public void CheckForLose(){
        if(heroes.Any(h => h.currentCell == exitPoint))
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
