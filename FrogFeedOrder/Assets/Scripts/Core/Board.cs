using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    
    private LevelManager levelManager ;
    private TileGrid grid;
    private GameObject foundObject;
    
    private void Awake()
    {
        Instance = this;
        
        levelManager = LevelManager.Instance;
        grid = GetComponentInChildren<TileGrid>();
    }

    public void FeedFrog(int direction,GameObject tile)
    {
        
    }
}
