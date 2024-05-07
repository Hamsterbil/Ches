using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetMoveDirections();
        GetValidMoves();
    }

    public override bool IsValidMove(Vector2Int newPosition)
    {
        List<Vector2Int> validMoves = GetValidMoves();
        return validMoves.Contains(newPosition);
    }

    public override Vector2Int[] GetMoveDirections()
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, 2), // Up 2
            new Vector2Int(1, 1), // Up Right
            new Vector2Int(-1, 1) // Up Left
        };

        return directions;
    }

    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        Vector2Int[] directions = GetMoveDirections();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int pos = currentPosition;
            pos += direction;

            if (!IswithinBounds(pos))
            {
                break;
            }

            if (IsSquareOccupiedByEnemy(pos))
            {
                validMoves.Add(pos);
                break
            }
        }
        return validMoves;
    }
}
