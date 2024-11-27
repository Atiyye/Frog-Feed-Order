using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Cell[] cells { get; private set; }

    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    [SerializeField] private GameObject cell;
    private int size => cells.Length;
    private int height => int.Parse(Grid.Rows.ToString());
    private int width => int.Parse(Grid.Columns.ToString());
    
    private int cellCount = 0;
    
    private void Start()
    {
       // DeleteAllChildren();
        CreateCell();
        CreateCellCoordinate();
    }

    private void CreateCell()
    {
        cellCount = width * height;
        
        for (int i = 0; i < cellCount; i++)
        {
            Instantiate(cell, Grid.transform);
        }
        
        cells = GetComponentsInChildren<Cell>();
    }
    
    private void CreateCellCoordinate()
    {
        for (int i = 0; i < height; i++)
        {
            // X AXIS
            for (int j = 0; j < width; j++)
            {
                cells[j].coordinates = new Vector3Int(j,i);
            }
        }
    }
    
    public void DeleteAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public int GetWidth()
    {
        return width;
    }
    
    public int GetSize()
    {
        return size;
    }
    
    public int GetHeight()
    {
        return height;
    }
    
}
