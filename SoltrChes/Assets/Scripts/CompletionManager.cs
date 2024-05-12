using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionManager : MonoBehaviour
{
    [SerializeField] private GameObject winmenu;
    [SerializeField] private GameObject losemenu;
    public void CheckCompletion()
    {

    }

    public void WinGame()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel() + 1);
        AudioManager.Instance.PlaySound("win");
        winmenu.SetActive(true);
    }

    public void LoseGame()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel());
        AudioManager.Instance.PlaySound("lose");
        losemenu.SetActive(true);
    }
}
