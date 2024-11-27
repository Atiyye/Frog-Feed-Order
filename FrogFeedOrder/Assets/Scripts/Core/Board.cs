using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileStateSO[] tileStates;

    private List<Tile> tiles = new List<Tile>();
    
    private void Awake()
    {
        Grid._rows = 5;
        Grid._columns = 5;
    }
}
