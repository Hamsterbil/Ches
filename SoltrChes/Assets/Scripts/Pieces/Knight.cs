using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
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
            new Vector2Int(1, 2), // Up Right
            new Vector2Int(-1, 2), // Up Left
            new Vector2Int(1, -2), // Down Right
            new Vector2Int(-1, -2), // Down Left
            new Vector2Int(2, 1), // Right Up
            new Vector2Int(-2, 1), // Left Up
            new Vector2Int(2, -1), // Right Down
            new Vector2Int(-2, -1) // Left Down
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
