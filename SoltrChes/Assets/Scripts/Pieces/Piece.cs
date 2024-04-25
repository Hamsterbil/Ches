using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    protected Vector2Int currentPosition;
    protected bool isWhite;

    public abstract bool IsValidMove(Vector2Int newPosition);
 
    public void Move(Vector2Int newPosition)
    {
        if (IsValidMove(newPosition))
        {
            currentPosition = newPosition;
        }
    }
    public Vector2Int GetCurrentPosition()
    {
        return currentPosition;
    }
    public abstract Vector2Int[] GetMovedirections();
    public abstract List<Vector2Int> GetValidMoves();

    protected bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < LevelManager.Instance.GetBoardSize()[0] && position.y >= 0 && position.y < LevelManager.Instance.GetBoardSize()[1];
    }

    protected bool IsSqaureEmpty(Vector2Int position)
    {
        return true; // placeholder
    }

    protected bool IsSqaureOccupiedByEnemy(Vector2Int position)
    {
        return false; // placeholder
    }
}
