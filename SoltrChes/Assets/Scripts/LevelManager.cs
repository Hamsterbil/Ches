using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private int currentLevel;
    private LevelData[] levels;
    private BoardData board;
    private PieceData[] pieces;
    private Square[] squares;

    public GameObject squarePrefab;
    public GameObject[] piecePrefabs;

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

    private void LoadJSON()
    {
        string jsonPath = Application.dataPath + "/Scripts/JSON/Levels.json";
        string json = System.IO.File.ReadAllText(jsonPath);
        levels = JsonConvert.DeserializeObject<LevelData[]>(json);
    }

    public void LoadLevel(int level)
    {
        DeleteLevel();
        currentLevel = level;
        LevelData levelData = levels[currentLevel - 1];
        board = levelData.board;
        pieces = levelData.pieces.ToArray();

        CreateBoard(board.width, board.height);
        SetPieces();
    }

    private void DeleteLevel()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateBoard(int width, int height)
    {
        squares = new Square[width * height];
        GameObject squaresParent = new GameObject("Squares");
        squaresParent.transform.parent = this.transform;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                GameObject squareObject = Instantiate(squarePrefab, position, Quaternion.identity, squaresParent.transform);
                squareObject.name = "Square " + x + ", " + z;
                if ((x + z) % 2 == 0)
                {
                    squareObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f);
                    squares[x * height + z] = new Square(new int[] { x, z }, "White", false, null);
                }
                else
                {
                    squareObject.GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
                    squares[x * height + z] = new Square(new int[] { x, z }, "Black", false, null);
                }
            }
        }
    }

    private void SetPieces()
    {
        foreach (PieceData piece in pieces)
        {
            switch (piece.type)
            {
                case "Queen":
                    CreatePiece(piece, piecePrefabs[0]);
                    break;
                case "Pawn":
                    CreatePiece(piece, piecePrefabs[1]);
                    break;
                case "Rook":
                    CreatePiece(piece, piecePrefabs[2]);
                    break;
                case "King":
                    CreatePiece(piece, piecePrefabs[3]);
                    break;
                case "Bishop":
                    CreatePiece(piece, piecePrefabs[4]);
                    break;
                case "Knight":
                    CreatePiece(piece, piecePrefabs[5]);
                    break;
            }
        }
    }

    private void CreatePiece(PieceData piece, GameObject prefab)
    {
        Vector3 position = new Vector3(piece.position[0], 0.5f, piece.position[1]);
        GameObject pieceObject = Instantiate(prefab, position, Quaternion.identity, this.transform);
    }

    public int[] GetBoardSize()
    {
        return new int[] { board.width, board.height };
    }

    public string GetLevelName()
    {
        return levels[currentLevel - 1].levelName;
    }

    public LevelData[] GetLevels()
    {
        return levels;
    }
}
