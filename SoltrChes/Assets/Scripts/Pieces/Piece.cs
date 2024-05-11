using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Vector2Int currentPosition;
    protected bool isWhite;
    protected bool hasMoved;
    protected Vector2Int[] directions;
    public List<Vector2Int> legalMoves;

    public void InitPiece(Vector2Int position, bool isWhite)
    {
        currentPosition = position;
        this.isWhite = isWhite;
        hasMoved = false;
        directions = GetMoveDirections();
    }

    public abstract Vector2Int[] GetMoveDirections();
    public abstract List<Vector2Int> GetValidMoves();

    public void Move(Square newSquare)
    {
        Square currentSquare = LevelManager.Instance.GetSquare(currentPosition);
        currentSquare.isOccupied = false;
        if (newSquare.piece && this != newSquare.piece)
        {
            Destroy(newSquare.piece.gameObject);
        }
        newSquare.piece = this;

        Vector2Int newPosition = newSquare.position;
        currentPosition = newPosition;
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.y);
        LevelManager.Instance.RemoveHighlights(this);
        GameManager.Instance.CheckCompletion();
    }
}
