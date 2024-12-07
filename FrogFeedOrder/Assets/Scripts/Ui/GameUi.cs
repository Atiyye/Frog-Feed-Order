using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    public static GameUi Instance { get; private set; }
    
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLevelText(int count)
    {
        levelText.text = (count + 1).ToString();
    }
    
    public void UpdateMovesText(int count)
    {
        movesText.text = count.ToString();
        if (count == 0)
        {
            GameManager.Instance.LevelFailed();
        }
    }
}
