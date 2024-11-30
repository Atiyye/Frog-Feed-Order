using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    
    [Header("Tile")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private List<TileStateSO> frogStates;
    [SerializeField] private List<TileStateSO> arrowStates;
    [SerializeField] private List<TileStateSO> grapeStates;
   
    [Header("Node")] 
    [SerializeField] private List<TileStateSO> node;
    
    private List<Tile> tiles = new List<Tile>();
    private LevelManager levelManager ;
    private TileGrid grid;
    
    private int objectTypeCount;

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
        CreateTile();
    }

    
    private void CreateTile()
    {
       // Shuffle(frogStates);
      //  Shuffle(grapeStates);
      //  Shuffle(arrowStates);
        
        for (int i = 0; i < Consts.Count.colorCount; i++)
        {
            Cell randomCell = grid.GetRandomCell();
            CreateFrog(i, randomCell);
            CreateGrapeAndArrow(i, randomCell);
        }
    }

    private void CreateFrog(int i, Cell randomCell)
    {
        Tile tile = Instantiate(tilePrefab, randomCell.transform);
        tile.SpawnTile(randomCell);
        tile.SetState(frogStates[i],randomCell,Grid._columns,Grid._rows);
        node.Add(frogStates[i]);
        tiles.Add(tile);
    }

    private void CreateGrapeAndArrow(int i, Cell randomCell)
    {
        for (int j = 0; j < RandomNodeLength(); j++)
        {
            int percent = Random.Range(0, 100);

            if (percent <= 80 || j == RandomNodeLength() - 1)
            {
                Tile tile = Instantiate(tilePrefab, randomCell.transform);
                tile.SpawnTile(randomCell);
                tile.SetState(grapeStates[i],randomCell,Grid._columns,Grid._rows);
                node.Add(grapeStates[i]);
                tiles.Add(tile);
            }
            else
            {
                Tile tile = Instantiate(tilePrefab, randomCell.transform);
                tile.SpawnTile(randomCell);
                tile.SetState(arrowStates[i],randomCell,Grid._columns,Grid._rows);
                node.Add(arrowStates[i]);
                tiles.Add(tile);
            }
        }
    }

    private int RandomNodeLength()
    {
        int height = int.Parse(Grid.Rows.ToString());
        int width = int.Parse(Grid.Columns.ToString());

        int maxLenght = (width * height) - 1;
        int nodeLenght = Random.Range(1, maxLenght);
        return nodeLenght;
    }
    
    private void Shuffle(List<TileStateSO> list)
    {
        List<TileStateSO> shuffled = new List<TileStateSO>();
        List<TileStateSO> temp = new List<TileStateSO>();
        temp.AddRange(list);
        
        for (int i = 0; i < list.Count; i++)
        {
            shuffled.Add(temp[ShuffleList()[i]]);
            temp.RemoveAt(ShuffleList()[i]);
        }
        list.Clear();
        list.AddRange(shuffled);
    }

    private List<int> ShuffleList()
    {
        List<int> shuffle = new List<int> { 0, 1, 2, 3 };
        
        shuffle = shuffle.OrderBy(i => Guid.NewGuid()).ToList();

        return shuffle;
    }
}
