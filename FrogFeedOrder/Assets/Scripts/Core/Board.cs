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
    
    [Header("Tile")]
    [SerializeField] private Tile tilePrefab ;
    [SerializeField] private List<TileStateSO> frogStates;
    [SerializeField] private List<TileStateSO> arrowStates;
    [SerializeField] private List<TileStateSO> grapeStates; 
    List<int> shuffleList = new List<int> { 0, 1, 2, 3 };
    List<TileStateSO> shuffled = new List<TileStateSO>();
   
    [Header("Node")] 
    [SerializeField] private List<TileStateSO> node;
    
    private List<Tile> tiles = new List<Tile>();
    private LevelManager levelManager ;
    private TileGrid grid;
    
    private int objectTypeCount;
    private int nodeDirection=0;
    private Cell randomCell=null;
    private int matchingCellCount;

    private void Awake()
    {
        levelManager = LevelManager.Instance;
        grid = GetComponentInChildren<TileGrid>();
       
        Grid._rows = levelManager.row;
        Grid._columns = levelManager.column;

    }

    private void Start()
    {
        CreateNode();
    }
    
    private void CreateNode()
    {
        Shuffle(frogStates,grapeStates,arrowStates);
        CreateTile();
    }

    private void CreateTile()
    {
        for (int i = 0; i < 1; i++)
        {
            randomCell = grid.GetRandomCell();
            CreateFrog(i, randomCell);
            CreateGrapeAndArrow(i, randomCell);
        }
    }

    private void CreateFrog(int i, Cell randomCell)
    {
        CreateContent(i, randomCell, frogStates);
    }
    
    
    private void CreateGrapeAndArrow(int i, Cell randomCell)
    {
        for (int j = 0; j < RandomNodeLength(); j++)
        {
            randomCell = NodeDirection(randomCell);
            int percent = Random.Range(0, 100);

            if (percent <= 80 || j == RandomNodeLength() - 1)
            {
                CreateContent(i, randomCell, grapeStates);
            }
            else
            {
                CreateContent(i, randomCell, arrowStates);
            } 
            
        }
    }

    private void CreateContent(int i, Cell randomCell,List<TileStateSO> tileStateList)
    {
        Tile tile = Instantiate(tilePrefab, randomCell.transform);
        tile.SpawnTile(randomCell);
        tile.SetState(tileStateList[i],randomCell,Grid._columns,Grid._rows,nodeDirection);
        node.Add(tileStateList[i]);
        tiles.Add(tile);
        nodeDirection = tile.rotate.direction;
    }
    
    private int RandomNodeLength()
    {
        int height = int.Parse(Grid.Rows.ToString());
        int width = int.Parse(Grid.Columns.ToString());

        int maxLenght = (width * height * Consts.Count.colorCount) - 1;
        int nodeLenght = Random.Range(height, maxLenght);
        return nodeLenght;
    }
    
    private void Shuffle(List<TileStateSO> listFrogStates,List<TileStateSO> listGrapeStates,List<TileStateSO> listArrowStates)
    {
        shuffleList = shuffleList.OrderBy(i => Guid.NewGuid()).ToList();
        ShuffleList(listFrogStates);
        ShuffleList(listGrapeStates);
        ShuffleList(listArrowStates);
    }

    private void ShuffleList(List<TileStateSO> list)
    {
        for (int i = 0; i < Consts.Count.colorCount; i++)
        {
            shuffled.Add(list[shuffleList[i]]);
        }
        list.Clear();
        list.AddRange(shuffled);
        shuffled.Clear();
    }

    private Cell NodeDirection(Cell randomCell)
    {
        Vector3Int newCoordinates = randomCell.coordinates;
        if (nodeDirection == Consts.Rotate.up)
            // Up and down changed places because the axis was rotated
            newCoordinates = randomCell.coordinates + Vector3Int.down;
        else if(nodeDirection == Consts.Rotate.down)
            // Up and down changed places because the axis was rotated
            newCoordinates = randomCell.coordinates + Vector3Int.up;
        else if(nodeDirection == Consts.Rotate.left)
            newCoordinates = randomCell.coordinates + Vector3Int.left;
        else if(nodeDirection == Consts.Rotate.right)
            newCoordinates = randomCell.coordinates + Vector3Int.right;

        
        Cell foundCell = grid.cells.FirstOrDefault(obj => obj.coordinates == newCoordinates);
        return foundCell;
    }
}
