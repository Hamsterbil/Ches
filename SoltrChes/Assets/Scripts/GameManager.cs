using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private CompletionManager completionManager;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentLevel = 1;
        multiplier = 5;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(int levelNumber)
    {
        currentLevel = levelNumber;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            InitializeFields();
            ChangeLevel(currentLevel);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeFields()
    {
        timer = FindObjectOfType<Timer>();
        completionManager = FindObjectOfType<CompletionManager>();
        //Sorry men kan ikke få det til at virke på nogen anden måde, alligevel virker det, så vi kører med det
        GameObject.Find("EscapeMenu").SetActive(true);
        menu = GameObject.Find("EscapeMenu");
        menu.SetActive(false);
        pieceController = FindObjectOfType<PieceController>();
        displayLevelName = FindObjectOfType<DisplayLevelName>();
        displayPoints = FindObjectOfType<DisplayPoints>();
        completionManager = FindObjectOfType<CompletionManager>();
    }

    void Update()
    {
        if (menu && Input.GetKeyDown(KeyCode.Escape))
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