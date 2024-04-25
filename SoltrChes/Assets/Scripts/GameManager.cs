using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        LevelManager.Instance.LoadLevel(1);
    }

    void Update()
    {
        //If spacebar clicked, go to next level
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Loop back to level 1 if current level is the last level
            if (LevelManager.Instance.GetCurrentLevel() == LevelManager.Instance.GetLevels().Length)
            {
                LevelManager.Instance.LoadLevel(1);
                return;
            }
            LevelManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel() + 1);
        }
    }
}
