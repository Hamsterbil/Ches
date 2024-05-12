using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Vector2Int currentPosition;
    protected bool isWhite;
    protected bool hasMoved;
    public List<Vector2Int> legalMoves;

    public void InitPiece(Vector2Int position, bool isWhite)
    {
        currentPosition = position;
        this.isWhite = isWhite;
        hasMoved = false;
    }

    public abstract List<Vector2Int> GetValidMoves();

    public void Move(Square newSquare)
    {
        if (newSquare.piece && this != newSquare.piece)
        {
            LevelManager.Instance.RemovePiece(newSquare.piece);
        }
        newSquare.piece = this;

        Vector2Int newPosition = newSquare.position;
        currentPosition = newPosition;
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.y);

        LevelManager.Instance.RemoveHighlights(this);
        GameManager.Instance.CheckCompletion();
    }
}