using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    public Grid grid;
    
    public Vector3Int currentCell;

    public DOTweenAnimationTemplate moveAnimation;

    private void Start()
    {
        transform.position = grid.GetCellCenterWorld(currentCell);
    }

    public void Move(Vector3Int direction)
    {
        currentCell += direction;
        Debug.Log(direction);
        var targetPosition = grid.GetCellCenterWorld(currentCell);
        transform.DOMove(targetPosition, moveAnimation.duration).SetEase(moveAnimation.ease);
    }

    //! DEBUG ONLY
    void OnMove(InputValue value)
    {
        var inputVector = value.Get<Vector2>();
        var direction = new Vector3Int((int) inputVector.x, (int) inputVector.y, 0);
        Move(direction);
    }
}
