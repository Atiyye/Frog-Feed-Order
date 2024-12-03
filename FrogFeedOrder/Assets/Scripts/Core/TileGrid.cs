using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public static TileGrid Instance { get; private set; }
    
    [SerializeField] public List<Cell> cells;
    [SerializeField] public List<Tile> blueTiles;
    [SerializeField] public List<Tile> greenTiles;
    [SerializeField] public List<Tile> yellowTiles;
    [SerializeField] public List<Tile> redTiles;
    
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    private int height => int.Parse(Grid.Rows.ToString());
    private int width => int.Parse(Grid.Columns.ToString());
    
    private int cellCount = 0;
    
    private void Awake()
    {
        Instance = this;
        CreateCellCoordinate();
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
    
}
