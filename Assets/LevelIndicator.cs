using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    [SerializeField] private int _levelValue = 1; //[SerializeField] will make the private value visible in the inspector window.

    public int LevelValue
    {
        get { return _levelValue; }
        set
        {
            if (_levelValue != value)
            {
                _levelValue = value;
                UpdateLevelText();
            }
        }
    }

    void Start()
    {
        // Initial setup of the text
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        levelText.text = $"<b>Level: {_levelValue}</b>";
    }

    private void OnValidate()
    {
        // OnValidate() will update the text in the Editor when _levelValue is changed through the Inspector window, otherwise only scripts can update the value and text.
        UpdateLevelText();
    }
}
