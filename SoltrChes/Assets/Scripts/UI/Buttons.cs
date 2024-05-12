using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);  
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);  
    }

    public void OnRestartButton()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel());
    }

    public void OnNextLevelButton()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel() + 1); 
    }

}