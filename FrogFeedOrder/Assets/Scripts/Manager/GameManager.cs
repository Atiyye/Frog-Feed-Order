using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  set; }
    
    private TileGrid grid;
    public int moves;
    public int frogs;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LevelManager.Instance.LoadCurrentLevel();
        moves = (LevelManager.Instance.currentLevelIndex + 1) * 2 + Consts.Count.colorCount;
        frogs = Consts.Count.colorCount - 1;
        GameUi.Instance.UpdateLevelText(LevelManager.Instance.currentLevelIndex);
        GameUi.Instance.UpdateMovesText(moves);
    }
    
    public void LevelComplete()
    {
        LevelCompleteUi.Instance.LevelComplete(); 
    }

    public void LevelFailed()
    {
        LevelFailedUi.Instance.LevelFailed(); 
    }
    
    public void OnLevelComplete()
    {
        LevelManager.Instance.LoadNextLevel(); 
        moves = (LevelManager.Instance.currentLevelIndex + 1) * 2 + Consts.Count.colorCount;
        frogs = Consts.Count.colorCount - 1;
        GameUi.Instance.UpdateLevelText(LevelManager.Instance.currentLevelIndex);
        GameUi.Instance.UpdateMovesText(moves);
    }

    public void OnLevelFailed()
    {
        LevelManager.Instance.RestartLevel(); 
        moves = (LevelManager.Instance.currentLevelIndex + 1) * 2 + Consts.Count.colorCount;
        frogs = Consts.Count.colorCount - 1;
        GameUi.Instance.UpdateLevelText(LevelManager.Instance.currentLevelIndex);
        GameUi.Instance.UpdateMovesText(moves);
    }
}
