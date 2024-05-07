using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Vector2Int currentPosition;
    protected bool isWhite;
    public List<Vector2Int> validMoves;

    public void InitPiece(Vector2Int position, bool isWhite)
    {
        currentPosition = position;
        this.isWhite = isWhite;
        validMoves = new List<Vector2Int>();
    }

    public bool IsValidMove(Vector2Int newPosition)
    {
        return validMoves.Contains(newPosition);
    }

    public Vector2Int GetCurrentPosition()
    {
        return currentPosition;
    }

    public abstract Vector2Int[] GetMoveDirections();
    public abstract List<Vector2Int> GetValidMoves();

    protected bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < LevelManager.Instance.GetBoardSize()[0] && position.y >= 0 && position.y < LevelManager.Instance.GetBoardSize()[1];
    }

    protected bool IsSquareEmpty(Vector2Int position)
    {
        Square square = LevelManager.Instance.GetSquare(position);
        return !square.isOccupied;
    }

    protected bool IsSquareOccupiedByEnemy(Vector2Int position)
    {
        if (IsWithinBounds(position))
        {
            Square square = LevelManager.Instance.GetSquare(position);
            return square.isOccupied;
        }
        return false;
    }

    public void HighlightValidMoves()
    {
        LevelManager.Instance.RemoveHighlights();
        foreach (Vector2Int move in validMoves)
        {
            LevelManager.Instance.HighlightSquare(move);
        }
    }

    public void Move(Vector2Int newPosition)
    {
        Square currentSquare = LevelManager.Instance.GetSquare(currentPosition);
        Square newSquare = LevelManager.Instance.GetSquare(newPosition);

        currentSquare.isOccupied = false;
        //Destroy piece
        if (this != newSquare.piece)
        {
            Destroy(newSquare.piece.gameObject);
        }
        newSquare.isOccupied = true;
        newSquare.piece = this;

        currentPosition = newPosition;
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.y);
        LevelManager.Instance.RemoveHighlights();
    }
}
