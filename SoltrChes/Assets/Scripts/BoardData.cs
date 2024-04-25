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
}