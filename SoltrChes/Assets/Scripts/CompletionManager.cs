using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionManager : MonoBehaviour
{
    public void CheckCompletion()
    {
        
    }

    public void WinGame()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel() + 1);
    }

    public void LoseGame()
    {
        GameManager.Instance.ChangeLevel(GameManager.Instance.GetCurrentLevel());
    }
}
