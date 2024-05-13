using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int currentLevel;
    public int multiplier;
    public int totalPoints;
    public Timer timer;
    [SerializeField] private DisplayLevelName displayLevelName;
    [SerializeField] private GameObject menu;
    [SerializeField] private PieceController pieceController;
    [SerializeField] private DisplayPoints displayPoints;
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
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (currentLevel == LevelManager.Instance.GetLevels().Length)
        //     {
        //         ChangeLevel(1);
        //         return;
        //     }
        //     ChangeLevel(currentLevel + 1);
        // }

        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     ChangeLevel(currentLevel);
        // }

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
    }

    public void ChangeLevel(int level)
    {
        Debug.Log("Changing level to " + level);
        if (level != currentLevel)
        {
            multiplier = 5;
        }
        currentLevel = level;
        LevelManager.Instance.LoadLevel(level);
        timer.ResetTimer();
        displayLevelName.DisplayLevel(currentLevel);
        displayPoints.DisplayPoint(totalPoints);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void CheckCompletion()
    {
        completionManager.CheckCompletion();
    }
}