using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionManager : MonoBehaviour
{
    [SerializeField] private GameObject winmenu;
    [SerializeField] private GameObject losemenu;
    public void CheckCompletion()
    {
        //Based on level type, check if the level is completed
        switch (LevelManager.Instance.GetCurrentLevel().type)
        {
            case "Chess":

                break;
            case "Solitaire":
                //If there is one piece left, win. If there are no legal moves left for every piece, lose
                if (LevelManager.Instance.GetPieces().Count == 1)
                {
                    WinGame();
                }
                else
                {
                    bool noMoves = true;
                    foreach (Piece piece in LevelManager.Instance.GetPieces())
                    {
                        if (piece.legalMoves.Count > 0)
                        {
                            noMoves = false;
                            break;
                        }
                    }
                    if (noMoves)
                    {
                        LoseGame();
                    }
                }

                break;
        }
    }

    public void WinGame()
    {
        if (LevelManager.Instance.GetCurrentLevel().isCompleted == false)
        {
            GameManager.Instance.totalPoints += 50 * GameManager.Instance.multiplier;
        }
        LevelManager.Instance.GetCurrentLevel().isCompleted = true;
        GameManager.Instance.timer.PauseTimer();
        AudioManager.Instance.PlaySound("win");
        winmenu.SetActive(true);
    }

    public void LoseGame()
    {
        GameManager.Instance.multiplier = Mathf.Max(0, GameManager.Instance.multiplier - 1);
        GameManager.Instance.timer.PauseTimer();
        AudioManager.Instance.PlaySound("lose");
        losemenu.SetActive(true);
    }
}
