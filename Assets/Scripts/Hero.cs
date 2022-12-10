using System.Linq;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public TileMaster tileMaster;
    public GameMaster gameMaster;
    
    public enum Class
    {
        Scout,
        Mage,
        Warrior,
        Cleric
    }

    public int baseStrength; // 2-3, warrior 2-4
    public int currentStrength;
    public Class @class;
    
    public Vector3Int goal;

    public void OnEndTurn()
    {
        currentStrength = baseStrength;
        Advance(goal);
    }

    void Advance(Vector3Int goalCell)
    {
        var currentNode = tileMaster.GetPathfindingNode(currentCell);
        var goalNode = tileMaster.GetPathfindingNode(goalCell);
        
        var path = Pathfinding.FindPath(currentNode, goalNode);
        var nextNode = path.ElementAt(Mathf.Min(1, path.Count - 1));
        var direction = nextNode.ToVector3Int() - currentCell;
        
        Debug.Log(direction);
        
        Move(currentCell + direction);
    }

    public void Move(Vector3Int cell)
    {
        currentCell = cell;
        var targetPosition = tileMaster.tilemap.GetCellCenterWorld(currentCell);
        transform.DOMove(targetPosition, moveAnimation.duration).SetEase(moveAnimation.easeType);
    }

    public Vector3Int currentCell;

    public DOTweenAnimationTemplate moveAnimation;

    private void Start()
    {
        transform.position = tileMaster.tilemap.GetCellCenterWorld(currentCell);
    }

    public void TakeDamage(int damage)
    {
        currentStrength -= damage;
        if (currentStrength <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        GameMaster.Instance.heroes.Remove(this);
        Destroy(gameObject);
    }
}
