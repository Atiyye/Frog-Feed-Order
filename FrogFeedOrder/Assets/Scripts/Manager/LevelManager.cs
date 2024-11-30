using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    public uint column = 4;
    public uint row = 4;
    public int level = 6;
    
    private void Awake()
    {
        Instance = this;
        
        column = 5;
        row = 5;
    }
    
}
