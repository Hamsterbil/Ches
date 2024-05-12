using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int currentLevel;
    [SerializeField] private Timer timer;
    [SerializeField] private DisplayLevelName displayLevelName;
    [SerializeField] private GameObject menu;
    [SerializeField] private PieceController pieceController;
    public CompletionManager completionManager;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeLevel(currentLevel);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)

            {
                pieceController.checkInputs = true;
                menu.SetActive(false);
                timer.ResumeTimer();
            }
            else
            {
                pieceController.checkInputs = false;
                menu.SetActive(true);
                timer.PauseTimer();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            completionManager.WinGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            completionManager.LoseGame();
        }

    }

    public void ChangeLevel(int level)
    {
        Debug.Log("Changing level to " + level);
        currentLevel = level;
        LevelManager.Instance.LoadLevel(level);
        timer.ResetTimer();
        displayLevelName.DisplayLevel(currentLevel);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void CheckCompletion()
    {

    }
}
