using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(SpriteRenderer))]
public class GridCursor : MonoBehaviour
{
    public TileMaster tileMaster;
    public new Camera camera;
    private Mouse mouse;
    private SpriteRenderer spriteRenderer;
    
    public Vector3Int position;
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
        if (newPosition != position)
        {
            var targetWorldPosition = tileMaster.tilemap.GetCellCenterWorld(newPosition);
            transform.DOMove(targetWorldPosition, animationTemplate.duration).SetEase(animationTemplate.easeType);
            position = newPosition;
        }
        
        var tile = tileMaster.tilemap.GetTile<Tile>(position);
        if (tile != null && tileMaster.GetTileGameData(tile).passable)
        {
            spriteRenderer.DOColor(Color.green, 0.1f);
        }
        else
        {
            spriteRenderer.DOColor(Color.red, 0.1f);
        }
    }
    public void ChangeVisibility(bool b){
        spriteRenderer.enabled = b;
    }
}
