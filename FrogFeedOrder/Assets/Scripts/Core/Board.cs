using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    
    private LevelManager levelManager ;
    private TileGrid grid;
    private GameObject foundObject;
    private GameObject cell;
    private Vector3Int coordinates;
    private Vector3Int newCoordinates;
    private Material tileMaterial;
    private Transform newTile;
    private int nodeDirection;
    
    private void Awake()
    {
        Instance = this;
        
        levelManager = LevelManager.Instance;
        grid = GetComponentInChildren<TileGrid>();
    }

    public void FeedFrog(int direction,GameObject tile)
    {
        nodeDirection = direction;
        newCoordinates = coordinates;
        
        cell = tile.transform.parent.gameObject;
        coordinates = cell.GetComponent<Cell>().coordinates;
        tileMaterial = tile.GetComponent<Tile>().tileMaterial;
    
        NodeDirection(tileMaterial,tile);
    }

    private void NodeDirection(Material material, GameObject tile)
    {
        newCoordinates = coordinates;
       
        for (int i = 0; i < grid.NodeCount(tile) - 1; i++)
        {
            bool isFrog = GetLastChild(tile.transform).name == Consts.Type.frog;
            Vector3Int coord = GetCoord(nodeDirection, isFrog);
            
            if (!LastChildControl(material,coord)) break;
            
        }
    }

    private bool  LastChildControl(Material material,Vector3Int coord)
    {
        newCoordinates += coord;
        Debug.LogError("coord: "+newCoordinates);
        
        newTile = GetLastChild(GetObjectAtCoordinates(newCoordinates).transform);
        
        if (material == newTile.GetComponent<Tile>().tileMaterial)
        {
            Debug.LogError("material aynı");
            if (GetLastChild(newTile).name == Consts.Type.arrow) 
            {
                nodeDirection = int.Parse(GetLastChild(newTile).gameObject.transform.localRotation.eulerAngles.y.ToString());
                nodeDirection = (Consts.Rotate.left + nodeDirection) % 360;
            }
            //Animasyon
            return true;
        }
        else
        {
            Debug.LogError("material farklı");
            newCoordinates = coordinates;
            //animasyon
            return false; 
        } 
    }

    private Vector3Int GetCoord(int direction, bool isFrog)
    {
        switch (direction)
        {
            case Consts.Rotate.up:
                return isFrog ? Vector3Int.down : Vector3Int.up;
            case Consts.Rotate.down:
                return isFrog ? Vector3Int.up : Vector3Int.down;
            case Consts.Rotate.left:
                return Vector3Int.left;
            case Consts.Rotate.right:
                return Vector3Int.right;
            default:
                return Vector3Int.zero; 
        }
    }
    
    private GameObject GetObjectAtCoordinates(Vector3Int coordinates)
    {
        foreach (var cell in grid.cells)
        {
            if (cell.coordinates == coordinates)
            {
                return cell.gameObject;
            }
        }
        return null; 
    }
    
    private Transform GetLastChild(Transform parent)
    {
        return parent.GetChild(parent.childCount - 1);
    }
}
