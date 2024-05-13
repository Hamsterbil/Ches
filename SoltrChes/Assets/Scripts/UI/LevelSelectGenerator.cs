using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LevelSelectGenerator : MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public Transform levelButtonContainer;

    public void GenerateLevelButtons()
    {
        if (levelButtonContainer.childCount == 0)
        {

            string jsonPath = Application.dataPath + "/Scripts/JSON/Levels.json";
            string json = System.IO.File.ReadAllText(jsonPath);
            LevelData[] levels = JsonConvert.DeserializeObject<LevelData[]>(json);

            foreach (LevelData level in levels)
            {
                Debug.Log(level.levelName);
                GameObject levelButton = Instantiate(levelButtonPrefab, levelButtonContainer);
                Button button = levelButton.GetComponent<Button>();
                button.onClick.AddListener(() => GameManager.Instance.StartGame(level.levelNumber));

                TMPro.TMP_Text _levelNameText = levelButton.GetComponentInChildren<TMPro.TMP_Text>();
                _levelNameText.text = level.levelName;
                _levelNameText.color = level.isCompleted ? Color.green : Color.red;
            }
        }
    }
}
