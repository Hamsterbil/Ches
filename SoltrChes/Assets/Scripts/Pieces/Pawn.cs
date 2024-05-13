using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    void Start()
    {
        GetMoveDirections();
    }

    public override Vector2Int[] GetMoveDirections()
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1), // Up Left
            new Vector2Int(0,1), // up
            new Vector2Int(0,2) // up 2
        };

        return directions;
    }

    public override List<Vector2Int> GetValidMoves()
    {
        validMoves = new List<Vector2Int>();
        Vector2Int[] directions = GetMoveDirections();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition;
            pos += direction;

            if (!IsWithinBounds(pos))
            {
                break;
            }
            if (IsSquareEmpty(pos))
            {
                validMoves.Add(pos);
                break;
            }
            if (direction == directions[3] && piece.hasMoves = false)
            {
                validMoves.Add(pos);
                break;
            }
        }
        return validMoves;
    }
}
