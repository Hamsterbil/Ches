using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
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
            new Vector2Int(1, -1), // Down Right
            new Vector2Int(-1, -1) // Down Left
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

            while (true)
            {
                pos += direction;

                if (!IsWithinBounds(pos))
                {
                    break;
                }

                if (IsSquareOccupiedByEnemy(pos))
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
