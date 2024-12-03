using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int levelCount;
    [SerializeField] private List<GameObject> levels;
    
    void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        Instantiate(LevelManager.Instance.levelList[levelCount], LevelManager.Instance.gameObject.transform);

    }
}
