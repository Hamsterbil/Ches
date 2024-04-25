using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    // Start is called before the first frame update

    public override bool IsValidMove(Vector2Int newPosition)
    {
        List<Vector2Int> validMoves = GetValidMoves();
        return validMoves.Contains(newPosition);
    }
    public override Vector2Int[] GetMovedirections()
    {
        Vector2int[] directions = new Vector2int[]
        {
            new Vector2int (1, 0) // Right
            new Vector2int (-1, 0) // Left
            new Vector2int (0, 1) // Up
            new Vector2int (0, -1) // Down
            new Vector2int (1, 1) // Up Right
            new Vector2int (-1, 1) // Up Left
            new Vector2int (1, -1) // Down Right
            new Vector2int (-1, -1) // Down Left
        };

        return directions;
    }
    public override List<Vector2Int> GetValidMoves();
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        
        Vector2Int[] directions = GetMovedirections();¨

        foreach (Vector2Int[] directions = GetMoveDirections)
        {
            Vector2Int currentPosition = GetCurrentPosition();

            while (true)
            {
                currentPosition += direction;

                if (!IsWithinBounds(currentPosition))
                {
                    break;
                }

                if (IsSqaureEmpty(currentPosition))
                {
                    validMoves.Add(currentPosition);
                }
                else
                {
                    if (IsSqaureOccupiedByEnemy(currentPosition))
                    {
                        validMoves.Add(currentPosition);
                    }
                    break;
                }

            }
        }
        return validMoves;
    }

}
