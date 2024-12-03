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
    private Material tileMaterial;
    
    private void Awake()
    {
        Instance = this;
        
        levelManager = LevelManager.Instance;
        grid = GetComponentInChildren<TileGrid>();
    }

    public void FeedFrog(int direction,GameObject tile)
    {
        cell = tile.transform.parent.gameObject;
        coordinates = cell.GetComponent<Cell>().coordinates;
        tileMaterial = tile.GetComponent<Tile>().tileMaterial;
        Debug.LogError(tileMaterial);

        NodeDirection(coordinates, direction, tileMaterial);
    }
    
    private void NodeDirection(Vector3Int coordinates, int nodeDirection, Material material)
    {
        Vector3Int newCoordinates = coordinates;
        if (nodeDirection == Consts.Rotate.up)
        {
            Transform newTile;
            newCoordinates = coordinates + Vector3Int.down;
            Debug.LogError("up: "+newCoordinates);
            newTile = GetLastChild(GetObjectAtCoordinates(newCoordinates).transform);
            if (material == newTile.GetComponent<Tile>().tileMaterial)
            {
                //Animasyon
            }
            else
            {
                //animasyon
            } 

        }
        else if (nodeDirection == Consts.Rotate.down)
            // Up and down changed places because the axis was rotated
        {
            newCoordinates = coordinates + Vector3Int.up;
            Debug.LogError("down: "+newCoordinates);
        }
        else if (nodeDirection == Consts.Rotate.left)
        {
            newCoordinates = coordinates + Vector3Int.left;
            Debug.LogError("left: "+newCoordinates);
        }
        else if (nodeDirection == Consts.Rotate.right)
        {
            newCoordinates = coordinates + Vector3Int.right;
            Debug.LogError("right: "+newCoordinates);
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
