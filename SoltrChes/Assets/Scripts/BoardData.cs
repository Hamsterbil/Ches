using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public int width;
    public int height;
}

public class Square
{
    public int[] position;
    public string color;
    public bool isOccupied;
    public PieceData piece;

    public Square(int[] position, string color, bool isOccupied, PieceData piece)
    {
        this.position = position;
        this.color = color;
        this.isOccupied = isOccupied;
        this.piece = piece;
    }
}