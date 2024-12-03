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
    }
    
}
