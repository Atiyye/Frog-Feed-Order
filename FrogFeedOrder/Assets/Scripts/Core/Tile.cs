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

    [SerializeField] public Material tileMaterial;
    [SerializeField] public string color;
    [SerializeField] private GameObject tileContent;
    
    private void Awake()
    {
        Instance = this;
    }
    
}
