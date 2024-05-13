using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelName : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _levelNameText;

    public void DisplayLevel(int level)
    {
        _levelNameText.text = LevelManager.Instance.GetLevels()[level - 1].levelName;
    }
}