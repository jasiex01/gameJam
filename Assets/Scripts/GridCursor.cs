using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(SpriteRenderer))]
public class GridCursor : MonoBehaviour
{
    public TileMaster tileMaster;
    public new Camera camera;
    private Mouse mouse;
    private SpriteRenderer spriteRenderer;

    public Color validColor = Color.green;
    public Color invalidColor = Color.red;
    
    public Vector3Int cell;
    public DOTweenAnimationTemplate animationTemplate;
    private void Awake()
    {
    mouse = Mouse.current;
    spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        var mousePosition = mouse.position.ReadValue();
        var mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition);
        var newPosition = tileMaster.tilemap.WorldToCell(mouseWorldPosition);
        if (newPosition != cell)
        {
            var targetWorldPosition = tileMaster.tilemap.GetCellCenterWorld(newPosition);
            transform.DOMove(targetWorldPosition, animationTemplate.duration).SetEase(animationTemplate.easeType);
            cell = newPosition;
        }
    }
    
    public void ChangeVisibility(bool b){
        spriteRenderer.enabled = b;
    }
    
    public void SetValid(bool b){
        spriteRenderer.color = b ? validColor : invalidColor;
    }
}