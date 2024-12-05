using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    
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
        bool isBreak = false;
        Vector3Int coord;
            
        for (int i = 0; i < grid.NodeCount(tile).Count - 1; i++)
        {
            bool isFrog = GetLastChild(tile.transform).name == Consts.Type.frog;
            coord = GetCoord(nodeDirection, isFrog);

            if (!LastChildControl(material, coord))
            {
                isBreak = true;
                break;
            }
        }
        if (!isBreak)
        {
          //  StartCoroutine(DeleteNode(tile));
        }
    }

    private bool LastChildControl(Material material,Vector3Int coord)
    {
        newCoordinates += coord;
        Debug.Log("Coord: "+newCoordinates);
        
        newTile = GetLastChild(GetObjectAtCoordinates(newCoordinates).transform);
        
        if (material == newTile.GetComponent<Tile>().tileMaterial)
        {
            Debug.Log("Material Same");
            if (GetLastChild(newTile).name == Consts.Type.arrow) 
            {
                nodeDirection = int.Parse(GetLastChild(newTile).gameObject.transform.localRotation.eulerAngles.y.ToString());
                nodeDirection = (Consts.Rotate.left + nodeDirection) % 360;
            }
            StartCoroutine(Animation.Instance.ContentsGrowthAnimate(newTile.gameObject,nodeDirection));
            return true;
        }
        else
        {
            Debug.Log("Material Different");
            newCoordinates = coordinates;
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
    
    private IEnumerator DeleteNode(GameObject oldTile)
    {
        List<Tile> tiles = grid.NodeCount(oldTile);
        yield return new WaitForSeconds(tiles.Count/10);
        
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(.2f);
            Transform cell = tiles[i].transform.parent;
            
            StartCoroutine(Animation.Instance.DeleteTileAnimate(tiles[i]));
            tiles[i].gameObject.SetActive(false);
            
            if (GetChildCount(cell) >= Consts.Count.gridMinChild)
            {
                Transform tile = GetLastChild(cell);
                Transform content = tile.GetChild(tile.childCount - 1);
                
                StartCoroutine(Animation.Instance.CreateContentAnimate(content));
                content.gameObject.SetActive(true);
            }
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
    
    public Transform GetLastChild(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            if (child.gameObject.activeSelf)
            {
                return child;
            }
        }
        return null;
    }
    
    private int GetChildCount(Transform parent)
    {
        int count = parent.childCount;
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            if (!child.gameObject.activeSelf)
            {
                count--;
            }
        }
        return count;
    }
}
