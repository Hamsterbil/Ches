using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0), // Right
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1), // Up Left
            new Vector2Int(1, -1), // Down Right
            new Vector2Int(-1, -1) // Down Left
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition + direction;
            // while (true)
            // {
            //     pos += direction;
            // }
        }
        return validMoves;
    }
}
