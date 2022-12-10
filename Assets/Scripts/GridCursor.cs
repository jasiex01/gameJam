using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class GridCursor : MonoBehaviour
    {
        public Grid grid;
        private Mouse mouse;
        
        public Vector3Int position;

        public DOTweenAnimationTemplate animationTemplate;

        private void Awake()
        {
            mouse = Mouse.current;
        }

        private void Update()
        {
            var mousePosition = mouse.position.ReadValue();
            var mouseWorldPosition = Camera.current.ScreenToWorldPoint(mousePosition);
            var newPosition = grid.WorldToCell(mouseWorldPosition);
            if (newPosition != position)
            {
                var targetWorldPosition = grid.GetCellCenterWorld(newPosition);
                transform.DOMove(targetWorldPosition, animationTemplate.duration).SetEase(animationTemplate.easeType);
                position = newPosition;
            }
        }
    }
}