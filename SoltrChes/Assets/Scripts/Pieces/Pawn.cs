using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override Vector2Int[] GetMoveDirections()
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1) // Up Left
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

            if (IsSquareOccupiedByEnemy(pos))
            {
                validMoves.Add(pos);
            }
        }
        return validMoves;
    }
}
