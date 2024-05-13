using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        GameManager.Instance.StartGame(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
        LevelManager.Instance.RemoveBoard();
    }

    public void OnRestartButton()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel());
    }

    public void OnNextLevelButton()
    {
        if (LevelManager.Instance.IsLastLevel())
        {
            //Jeppe kode her til hvad der skal ske efter sidste level:

            return;
        }
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel() + 1);
    }
}