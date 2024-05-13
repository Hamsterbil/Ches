using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0), // Right
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1) // Down
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition;

            while (true)
            {
                pos += direction;

                if (!LevelManager.Instance.IsWithinBounds(pos))
                {
                    break;
                }
                if (LevelManager.Instance.IsSquareOccupied(pos))
                {
                    validMoves.Add(pos);
                    break;
                }
                validMoves.Add(pos);
            }
        }
        return validMoves;
    }
}
