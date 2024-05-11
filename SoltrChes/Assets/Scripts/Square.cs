using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Square : MonoBehaviour
{
    public Vector2Int position;
    public bool isBlack;
    public bool isOccupied;
    public bool highlighted = false;
    public Piece piece;
    private GameObject squarePrefab;

    public void InitSquare(Vector2Int position, bool isBlack, bool isOccupied)
    {
        this.position = position;
        this.isBlack = isBlack;
        this.isOccupied = isOccupied;

        //Set square color
        ChangeColor(isBlack ? new Color(0.2f, 0.2f, 0.2f) : new Color(0.8f, 0.8f, 0.8f));

        //Change name based on position
        gameObject.name = "Square " + this.position[0] + ", " + this.position[1];
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}