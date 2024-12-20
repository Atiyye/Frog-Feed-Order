using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector3Int coordinates { get; set; }  
    public Tile tile { get; set; }

    public bool isEmpty => tile == null; 
    
    public bool isOccupied => tile != null;

    public string coordinate;

    private void Start()
    {
        coordinate = coordinates.ToString();
    }
}
