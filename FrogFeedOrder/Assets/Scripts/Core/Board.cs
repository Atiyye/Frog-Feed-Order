using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileStateSO[] tileStates;
  

    private List<Tile> tiles = new List<Tile>();

    private TileGrid grid;
    
    private int objectTypeCount;

    public uint column = 4;
    public uint row = 4;
    
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
       
        
        Grid._rows = column;
        Grid._columns = row;

        objectTypeCount = tileStates.Length / Consts.Count.typeCount;
    }

    private void Start()
    {
        CreateTile();
    }
    
    public void CreateTile()
    {
        for (int i = 0; i < objectTypeCount; i++)
        {
            Cell randomCell = grid.GetRandomCell();
            
            Tile tile = Instantiate(tilePrefab, randomCell.transform);
            tile.SpawnTile(randomCell);
            tile.SetState(tileStates[i],randomCell,column,row);
            tiles.Add(tile);
        }
    }
}
