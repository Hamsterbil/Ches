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
        Camera.main.transform.position = new Vector3(boardx / 2.2f, 13, -boardz / 2.5f);
        Camera.main.transform.rotation = Quaternion.Euler(65, 0, 0);
    }

    void Update()
    {
        if (checkInputs)
        {
            // Check if the player has clicked the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Piece piece = hit.collider.GetComponent<Piece>();
                    if (piece != null && LevelManager.Instance.GetSquare(piece.currentPosition).highlighted == false)
                    {
                        // Set the selected piece and its initial position
                        selectedPiece = piece;
                        clickedPiece = piece;
                        initialPosition = selectedPiece.transform.position;

                        // Highlight the valid moves of the piece
                        LevelManager.Instance.HighlightSquares(selectedPiece);
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

            // Check if the left mouse button is being held down
            if (Input.GetMouseButton(0) && selectedPiece != null)
            {
                Plane plane = new Plane(Vector3.up, initialPosition);
                //Draw plane
                Debug.DrawLine(initialPosition, new Vector3(initialPosition.x, initialPosition.y + 1, initialPosition.z), Color.red);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    Vector3 newPosition = ray.GetPoint(distance);
                    newPosition.y = 1f;
                    selectedPiece.transform.position = newPosition;

                    //If raycast hits piece, trigger animation
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Piece hoveredPiece = hit.collider.GetComponent<Piece>();
                        if (hoveredPiece != null && hoveredPiece != selectedPiece)
                        {
                            // Trigger animation on the hovered piece
                            hoveredPieceAnimator = hoveredPiece.GetComponent<Animator>();
                            if (hoveredPieceAnimator != null)
                            {
                                AudioManager.Instance.PlaySound("hover");
                                hoveredPieceAnimator.SetBool("HoveringWhileHolding", true);
                            }
                        }
                        else
                        {
                            // Reset the Animator parameter when not hovering over a piece while holding another
                            if (hoveredPieceAnimator != null)
                            {
                                hoveredPieceAnimator.SetBool("HoveringWhileHolding", false);
                            }
                        }
                    }
                    else
                    {
                        // Reset the Animator parameter when not hovering over a piece while holding another
                        if (hoveredPieceAnimator != null)
                        {
                            hoveredPieceAnimator.SetBool("HoveringWhileHolding", false);
                        }
                    }
                }
            }

            // Check if the left mouse button is released
            if (Input.GetMouseButtonUp(0) && selectedPiece != null)
            {
                // Check if the mouse is released over a valid move
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //Round the position to the nearest integer, check if valid move
                    Vector3 newPosition = hit.collider.transform.position;
                    Vector2Int targetPos = new Vector2Int(Mathf.FloorToInt(newPosition.x), Mathf.FloorToInt(newPosition.z));
                    Debug.Log(targetPos);
                    if (selectedPiece.legalMoves.Contains(targetPos))
                    {
                        // Move the piece to the new position
                        selectedPiece.Move(LevelManager.Instance.GetSquare(targetPos));
                    }
                    else
                    {
                        // Return the piece to its initial position if the move is invalid
                        AudioManager.Instance.PlaySound("invalidMove");
                        selectedPiece.transform.position = initialPosition;
                    }
                }
                else
                {
                    // Return the piece to its initial position if the mouse is released outside the board
                    selectedPiece.transform.position = initialPosition;
                }

                // Reset selected piece
                selectedPiece = null;
            }
        }
    }
}