using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    
    [Header("Node")] 
    [SerializeField] private List<TileStateSO> node;

    private LevelManager levelManager ;
    private TileGrid grid;
    
    private void Awake()
    {
        levelManager = LevelManager.Instance;
        grid = GetComponentInChildren<TileGrid>();
    }

}
