using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] public List<GameObject> levelList;
    public int currentLevelIndex;
    private GameObject currentLevel;
   

    private void Awake()
    {
        Instance = this;
        currentLevelIndex = 0;
    }
    
    public void LoadCurrentLevel()
    {
        LoadLevel(currentLevelIndex);
    }
    
    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadLevel(currentLevelIndex);
    }
    
    public void RestartLevel()
    {
        LoadCurrentLevel();
    }
    
    public GameObject LoadLevel(int index)
    {
        UnloadCurrentLevel();

        if (index >= 0 && index < levelList.Count)
        {
            currentLevel = Instantiate(levelList[index]);
            return currentLevel;
        }
        return null;
    }
    
    public void UnloadCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = null;
        }
    }
}
