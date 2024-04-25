using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int currentLevel;

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
    }

    void Start()
    {
        ChangeLevel(1);
    }

    void Update()
    {
        //If spacebar clicked, go to next level
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Loop back to level 1 if current level is the last level
            if (currentLevel == LevelManager.Instance.GetLevels().Length)
            {
                ChangeLevel(1);
                return;
            }
            ChangeLevel(currentLevel + 1);
        }
    }

    public void ChangeLevel(int level)
    {
        currentLevel = level;
        LevelManager.Instance.LoadLevel(level);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
