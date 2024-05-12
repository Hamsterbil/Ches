using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private int currentLevel;
    private LevelData[] levels;
    private LevelData levelData;
    private BoardData board;
    public List<Piece> pieces;
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
        levelData = levels[currentLevel - 1];
        board = levelData.board;
        pieces = new List<Piece>();

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
                Square squareComponent = squareObject.GetComponent<Square>();
                squareComponent.InitSquare(new Vector2Int(x, z), (x + z) % 2 == 0, false);

                squares[x * height + z] = squareComponent;
            }
        }
    }

    private void SetPieces()
    {
        foreach (PieceData piece in levelData.pieces)
        {
            switch (piece.type)
            {
                case "Pawn":
                    CreatePiece(piece, piecePrefabs[0]);
                    break;
                case "Bishop":
                    CreatePiece(piece, piecePrefabs[1]);
                    break;
                case "Knight":
                    CreatePiece(piece, piecePrefabs[2]);
                    break;
                case "Rook":
                    CreatePiece(piece, piecePrefabs[3]);
                    break;
                case "Queen":
                    CreatePiece(piece, piecePrefabs[4]);
                    break;
                case "King":
                    CreatePiece(piece, piecePrefabs[5]);
                    break;
            }
        }
        //Get all legal moves
        foreach (Piece piece in pieces)
        {
            piece.legalMoves = GetLegalMoves(piece);
        }
    }

    private void CreatePiece(PieceData piece, GameObject prefab)
    {
        // Vector3 position = new Vector3(piece.position[0], 0.5f, piece.position[1]);
        GameObject pieceObject = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity, this.transform);
        
        Piece pieceComponent = pieceObject.GetComponent<Piece>();
        Debug.Log(pieceComponent);
        // pieceComponent.InitPiece(new Vector2Int(piece.position[0], piece.position[1]), true);
        
        // pieces.Add(pieceComponent);
    }

    public void RemovePiece(Piece piece)
    {
        Square square = GetSquare(piece.currentPosition);
        square.isOccupied = false;
        Destroy(piece.gameObject);
    }

    public int[] GetBoardSize()
    {
        return new int[] { board.width, board.height };
    }
    public LevelData[] GetLevels()
    {
        return levels;
    }

    public string levelName()
    {
        return levels[currentLevel - 1].levelName;
    }

    public string levelType()
    {
        return levels[currentLevel - 1].type;
    }

    public Square GetSquare(Vector2Int position)
    {
        return squares[position.x * board.height + position.y];
    }

    public List<Piece> GetPieces()
    {
        return pieces;
    }

    public void HighlightSquares(Piece piece)
    {
        RemoveHighlights(piece);
        foreach (Vector2Int move in piece.legalMoves)
        {
            Square square = GetSquare(move);
            square.highlighted = true;
            square.ChangeColor(Color.red);
            //remove square piece collider
            if (square.isOccupied)
            {
                square.piece.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void RemoveHighlights(Piece piece)
    {
        //Every highlighted square
        foreach (Square square in squares)
        {
            if (square.highlighted)
            {
                square.highlighted = false;
                square.ChangeColor(square.isBlack ? new Color(0.2f, 0.2f, 0.2f) : new Color(0.8f, 0.8f, 0.8f));
                //add square piece collider
                if (square.isOccupied)
                {
                    square.piece.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public List<Vector2Int> GetLegalMoves(Piece piece)
    {
        List<Vector2Int> validMoves = piece.GetValidMoves();
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        switch (levels[currentLevel - 1].type)
        {
            case "Classic":
                legalMoves = validMoves;
                break;
            case "Solitaire":
                foreach (Vector2Int move in validMoves)
                {
                    if (IsSquareOccupied(move))
                    {
                        legalMoves.Add(move);
                    }
                }
                break;
            default:
                break;
        }

        return legalMoves;
    }

    public bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < GetBoardSize()[0] && position.y >= 0 && position.y < GetBoardSize()[1];
    }

    public bool IsSquareEmpty(Vector2Int position)
    {
        if (IsWithinBounds(position))
        {
            return !GetSquare(position).isOccupied;
        }
        return false;
    }

    public bool IsSquareOccupied(Vector2Int position)
    {
        if (IsWithinBounds(position))
        {
            return GetSquare(position).isOccupied;
        }
        return false;
    }
}
