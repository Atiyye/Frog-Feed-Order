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

    private TileGrid grid;
    private int objectTypeCount;
    
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        
        Grid._rows = 4;
        Grid._columns = 4;

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
            tile.SetState(tileStates[i]);
            tile.SpawnTile(randomCell);
            tiles.Add(tile);
        }
    }
    
    private void MoveTiles(Vector3Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool isChanged = false;

        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Transform child = grid.transform.GetChild(i);
        }
    }
}
