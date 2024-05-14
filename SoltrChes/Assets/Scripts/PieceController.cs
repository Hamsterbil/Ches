using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public bool checkInputs = true;
    private Piece selectedPiece;
    private Piece clickedPiece;
    private Vector3 initialPosition;
    private Animator hoveredPieceAnimator; // Animator component of the hovered piece

    private void Start()
    {
        //move camera to look at the middle of board from the top at a 45 degree angle
        int boardx = LevelManager.Instance.GetBoardSize()[0];
        int boardz = LevelManager.Instance.GetBoardSize()[1];
        Camera.main.transform.position = new Vector3(boardx / 2.2f, 8, -boardz / 8.5f);
        Camera.main.transform.rotation = Quaternion.Euler(65, 0, 0);
    }

    void Update()
    {
        if (!checkInputs) { return; }

        HandlePieceSelection();
        HandlePieceDragging();
        HandlePiecePlacement();
    }

    private void HandlePieceSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Piece piece = hit.collider.GetComponent<Piece>();
                if (piece != null && LevelManager.Instance.GetSquare(piece.currentPosition).highlighted == false)
                {
                    selectedPiece = piece;
                    clickedPiece = piece;
                    initialPosition = new Vector3(selectedPiece.currentPosition.x, 0.5f, selectedPiece.currentPosition.y);

                    LevelManager.Instance.HighlightSquares(selectedPiece);
                }
                else if (piece != null && LevelManager.Instance.GetSquare(piece.currentPosition).highlighted == true)
                {
                    clickedPiece.Move(LevelManager.Instance.GetSquare(piece.currentPosition));
                }
                else
                {
                    LevelManager.Instance.RemoveHighlights(clickedPiece);
                    clickedPiece = null;
                }

                Square square = hit.collider.GetComponent<Square>();
                if (square && square.highlighted && clickedPiece)
                {
                    clickedPiece.Move(square);
                }
            }
            else
            {
                // Remove highlights if clicked outside the board
                LevelManager.Instance.RemoveHighlights(clickedPiece);
                clickedPiece = null;
            }
        }
    }

    private void HandlePieceDragging()
    {
        if (Input.GetMouseButton(0) && selectedPiece != null)
        {
            Plane plane = new Plane(Vector3.up, initialPosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 newPosition = ray.GetPoint(distance);
                newPosition.y = 1f;
                selectedPiece.transform.position = newPosition;

                HandleHoveredPieceAnimation(ray);
            }
        }
    }

    private void HandleHoveredPieceAnimation(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Piece hoveredPiece = hit.collider.GetComponent<Piece>();
            if (hoveredPiece != null && hoveredPiece != selectedPiece)
            {
                hoveredPieceAnimator = hoveredPiece.GetComponent<Animator>();
                if (hoveredPieceAnimator != null)
                {
                    AudioManager.Instance.PlaySound("hover");
                    hoveredPieceAnimator.SetBool("HoveringWhileHolding", true);
                }
            }
            else
            {
                if (hoveredPieceAnimator != null)
                {
                    hoveredPieceAnimator.SetBool("HoveringWhileHolding", false);
                }
            }
        }
        else
        {
            if (hoveredPieceAnimator != null)
            {
                hoveredPieceAnimator.SetBool("HoveringWhileHolding", false);
            }
        }
    }

    private void HandlePiecePlacement()
    {
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Square square = hit.collider.GetComponent<Square>();
                Piece piece = hit.collider.GetComponent<Piece>();
                if (square || (piece && piece != selectedPiece))
                {
                    Vector2Int targetPos = square ? square.position : piece.currentPosition;
                    if (selectedPiece.legalMoves.Contains(targetPos))
                    {
                        selectedPiece.Move(LevelManager.Instance.GetSquare(targetPos));
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("invalidMove");
                        selectedPiece.transform.position = initialPosition;
                        if (hoveredPieceAnimator != null)
                        {
                            hoveredPieceAnimator.SetBool("HoveringWhileHolding", false);
                        }
                    }
                }
                else
                {
                    selectedPiece.transform.position = initialPosition;
                }
            }
            else
            {
                selectedPiece.transform.position = initialPosition;
            }

            selectedPiece = null;
        }
    }
}