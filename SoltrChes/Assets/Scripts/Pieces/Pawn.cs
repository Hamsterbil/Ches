using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1) // Up Left
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition + direction;
        }

        return validMoves;
    }
}
