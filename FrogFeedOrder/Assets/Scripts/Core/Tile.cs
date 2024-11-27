using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Tile Instance { get; private set; }
    public TileStateSO tileState { get; private set; }
    public Cell cell { get; private set; }

    [SerializeField] private Material tileMaterial;
    [SerializeField] private GameObject tileContent;
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetState(TileStateSO tileState,int number)
    {
        this.tileState = tileState;

        tileMaterial = tileState.tileMaterial;
        
        tileContent = Instantiate(tileState.tileContent, transform);
        tileContent.transform.localPosition = Vector3.zero;
        
        Renderer objectRenderer = tileContent.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material = tileState.objectMaterial;
        }
        else
        {
            Debug.LogWarning("Material is not accessible.");
        }
    }
}
