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
    public Cell cell { get; private set; }

    [SerializeField] private Material tileMaterial;
    [SerializeField] private GameObject tileContent;
    [SerializeField] private List<int> rotationAngle = new List<int>();
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetState(TileStateSO tileState)
    {
        this.tileState = tileState;
        
        ChangeMaterial(gameObject,tileState.tileMaterial);
        
        tileContent = Instantiate(tileState.tileContent, transform);
        ChangeMaterial(tileContent.gameObject,tileState.objectMaterial);
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

    public void ContentRotation(GameObject content, String contentType)
    {
        if (contentType != Consts.Type.Grape)
        {
            content.transform.Rotate(0f, rotationAngle[RandomRotation()], 0f);
        }
        else content.transform.rotation = Quaternion.identity;
    }
    
    private int RandomRotation()
    {
       int rotation = Random.Range(0, rotationAngle.Count);
       Debug.Log(rotationAngle[rotation]);
       return rotation;
    }
    
    public void SpawnTile(Cell cell)
    {
        if (cell.transform.childCount > Consts.Count.gridMinChild)
        {
            cell.tile.tileContent.gameObject.SetActive(false);
            
            transform.position = new Vector3(cell.tile.transform.position.x, cell.tile.transform.position.y + Consts.PosNumber.yPos,
                cell.tile.transform.position.z - Consts.PosNumber.zPos);
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
