using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public float normalMovementSpeed = 5f;
    public float boostedMovementSpeed = 10f;
    public float rotationSpeed = 2f;
    private Piece selectedPiece;
    private Piece clickedPiece;
    private Vector3 initialPosition;

    void Update()
    {
        // Check if the player has clicked the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Piece piece = hit.collider.GetComponent<Piece>();
                if (piece != null)
                {
                    // Set the selected piece and its initial position
                    selectedPiece = piece;
                    clickedPiece = piece;
                    initialPosition = selectedPiece.transform.position;

                    // Highlight the valid moves of the piece
                    piece.legalMoves = LevelManager.Instance.GetLegalMoves(selectedPiece);
                    LevelManager.Instance.HighlightSquares(selectedPiece);
                }
            }

            //If ray hits a highlighted square, move piece to that square
            if (Physics.Raycast(ray, out hit))
            {
                Square square = hit.collider.GetComponent<Square>();
                if (square && square.highlighted && clickedPiece)
                {
                    clickedPiece.Move(square);
                }
            } else
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 newPosition = ray.GetPoint(distance);
                newPosition.y = 1f;
                selectedPiece.transform.position = newPosition;
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
                newPosition.y = 0.5f;
                Vector2Int targetPos = new Vector2Int(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.z));
                if (selectedPiece.legalMoves.Contains(targetPos))
                {
                    // Move the piece to the new position
                    selectedPiece.Move(LevelManager.Instance.GetSquare(targetPos));
                }
                else
                {
                    // Return the piece to its initial position if the move is invalid
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

// float currentMovementSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? boostedMovementSpeed : normalMovementSpeed;

// // Movement
// Vector3 moveDirection = Vector3.zero;
// if (Input.GetKey(KeyCode.W))
//     moveDirection += transform.forward;
// if (Input.GetKey(KeyCode.S))
//     moveDirection -= transform.forward;
// if (Input.GetKey(KeyCode.D))
//     moveDirection += transform.right;
// if (Input.GetKey(KeyCode.A))
//     moveDirection -= transform.right;

// transform.position += moveDirection.normalized * currentMovementSpeed * Time.deltaTime;

// // Limit camera position
// Vector3 newPosition = transform.position;
// newPosition.x = Mathf.Clamp(newPosition.x, -7f, 7f);
// newPosition.z = Mathf.Clamp(newPosition.z, -7f, 7f);
// newPosition.y = Mathf.Clamp(newPosition.y, 0f, 7f);
// transform.position = newPosition;

// // Rotation (when right clicking)
// if (Input.GetMouseButton(1)) // Right mouse button
// {
//     float mouseX = Input.GetAxis("Mouse X");
//     float mouseY = Input.GetAxis("Mouse Y");

//     transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
//     transform.Rotate(Vector3.right, -mouseY * rotationSpeed, Space.Self);
// }
