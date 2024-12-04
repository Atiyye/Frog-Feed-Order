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
            }
        }
    }
    
    public List<Tile> NodeCount(GameObject tile)
    {
        string tileColor = tile.GetComponent<Tile>().color;
        List<Tile> tiles = new List<Tile>();

        switch (tileColor)
        {
            case Consts.Color.blue:
                tiles.AddRange(blueTiles);
                break;
            case Consts.Color.red:
                tiles.AddRange(redTiles);
                break;
            case Consts.Color.green:
                tiles.AddRange(greenTiles);
                break;
            case Consts.Color.yellow:
                tiles.AddRange(yellowTiles);
                break;
        }

        return tiles;
    }
}
