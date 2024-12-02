using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Cell[] cells { get; private set; }
    public static TileGrid Instance { get; private set; }
    
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    [SerializeField] private GameObject cell;
    private int size => cells.Length;
    private int height => int.Parse(Grid.Rows.ToString());
    private int width => int.Parse(Grid.Columns.ToString());
    
    private int cellCount = 0;
    
    private void Awake()
    {
        Instance = this;
    }
    
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
    
    public void CreateCellCoordinate()
    {
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x; 
                cells[index].coordinates = new Vector3Int(x,y);
                Debug.Log(cells[x].coordinates);
            }
        }
    }
    
    public Cell GetRandomCell()
    {
        int index = Random.Range(0, cells.Length);
        return cells[index];
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
