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
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1), // Up Left
            new Vector2Int(0,1), // up
            new Vector2Int(0,2) // up 2
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition + direction;

            if (!LevelManager.Instance.IsWithinBounds(pos))
            {
                break;
            }

            if (direction == directions[3] && hasMoved == false)
            {
                validMoves.Add(pos);
                break;
            }
        }
        return validMoves;
    }
}