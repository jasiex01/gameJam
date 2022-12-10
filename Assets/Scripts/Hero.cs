using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    public TileMaster tileMaster;
    
    public enum Class
    {
        Scout,
        Mage,
        Warrior,
        Cleric
    }

    public int baseStrength; // 2-3, warrior 2-4
    public int currentStrength;

    public Vector3Int goal;

    public void OnNextTurn()
    {
        currentStrength = baseStrength;
        Advance();   
    }

    void Advance()
    {
        var currentNode = tileMaster.GetPathfindingNode(currentCell);
        var goalNode = tileMaster.GetPathfindingNode(goal);
        
        var path = Pathfinding.FindPath(currentNode, goalNode);
        var nextNode = path.First();
        var direction = nextNode.ToVector3Int() - currentCell;
        
        currentCell += direction;
        var targetPosition = tileMaster.tilemap.GetCellCenterWorld(currentCell);
        transform.DOMove(targetPosition, moveAnimation.duration).SetEase(moveAnimation.easeType);
    }
    
    public Vector3Int currentCell;

    public DOTweenAnimationTemplate moveAnimation;

    private void Start()
    {
        transform.position = tileMaster.tilemap.GetCellCenterWorld(currentCell);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Onward!");
            OnNextTurn();
        }
    }
}
