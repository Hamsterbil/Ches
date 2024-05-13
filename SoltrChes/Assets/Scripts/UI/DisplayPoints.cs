using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _pointText;

    public void DisplayPoint(int Points)
    {
        _pointText.text = GameManager.Instance.totalPoints + " points";
    }
}
