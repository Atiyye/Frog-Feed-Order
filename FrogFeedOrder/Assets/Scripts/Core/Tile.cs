using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public static Tile Instance { get; private set; }
    public TileStateSO tileState { get; private set; }
    
    public RandomRotate rotate;
    public Cell cell { get; private set; }

    [SerializeField] private Material tileMaterial;
    [SerializeField] private GameObject tileContent;
    
    private void Awake()
    {
        Instance = this;
        rotate = GetComponentInChildren<RandomRotate>();
    }
    public void SetState(TileStateSO tileState,Cell cell,uint column,uint row,int nodeDirection)
    {
        this.tileState = tileState;
        
        ChangeMaterial(gameObject,tileState.tileMaterial);
        
        tileContent = Instantiate(tileState.tileContent, transform);
        ChangeMaterial(tileContent.gameObject,tileState.objectMaterial);
        
        rotate.ContentRotation(tileContent.gameObject, tileState.objectType,
            cell.coordinates,column,row,nodeDirection);
    }
    private void ChangeMaterial(GameObject gameObject,Material material)
    {
        if (gameObject != null && material != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }
    public void SpawnTile(Cell cell)
    {
        if (cell.transform.childCount > Consts.Count.gridMinChild)
        {
            cell.tile.tileContent.gameObject.SetActive(false);
            
            transform.position = new Vector3(cell.tile.transform.position.x, cell.tile.transform.position.y + Consts.PosNumber.yPos,
                cell.tile.transform.position.z - Consts.PosNumber.zPos);
            
            this.cell = cell;
            this.cell.tile = this;
        }
        else
        {
            this.cell = cell;
            this.cell.tile = this;
            transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y + Consts.PosNumber.yPos,
                cell.transform.position.z - Consts.PosNumber.zPos);
        }
       
    }
}
