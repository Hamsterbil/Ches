using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private LevelData[] levels;
    private BoardData board;
    private PieceData[] pieces;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadJSON();
    }

    // Parse json

    private void LoadJSON()
    {
        // string json = Resources.Load<TextAsset>("Levels").text;
        // levels = JsonHelper.FromJson<LevelData>(json);
    }

    public void LoadLevel(int level)
    {
        LevelData levelData = levels[level - 1];
        board = levelData.board;
        pieces = levelData.pieces.ToArray();

        // Create board blocks and pieces
        CreateBoard();
        SetPieces();
    }

    private void CreateBoard()
    {
        // Create board blocks
    }

    private void SetPieces()
    {
        // Set pieces on board
    }
}
