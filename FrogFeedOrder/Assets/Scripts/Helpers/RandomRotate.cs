using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotate : MonoBehaviour
{ 
    [SerializeField] public List<int> rotationAngle = new List<int>();
    private void Awake()
    {
        //board = GetComponentInChildren<Board>();
       
    }

    public void ContentRotation(GameObject content, String contentType, Vector3Int coordinate,uint column,uint row)
    {
        if (contentType != Consts.Type.grape)
        {
            RandomRotation(coordinate,column,row);
            float rotationIndex = rotationAngle[CreateRotate()];
            
            Debug.LogError("coordinate: "+coordinate);
            content.transform.localRotation = Quaternion.Euler(0, rotationIndex, 0);
            
;            Debug.LogError("Local Euler Angles: " + content.transform.eulerAngles);
            Debug.LogError("Global Rotation: " + content.transform.rotation.eulerAngles);
        }
        else content.transform.localRotation = Quaternion.identity;
    }
    
    private void RandomRotation(Vector3Int coordinate,uint column,uint row)
    {
        RotateListClear();
        CoordinateControl(coordinate,column,row);
    }

    private int CreateRotate()
    {
        int rotation = Random.Range(0, rotationAngle.Count);
        return rotation;
    }

     private void CoordinateControl(Vector3Int coordinate,uint column,uint row)
    {
        if (coordinate.x == Consts.Coordinates.start)
        {
            rotationAngle.Remove(Consts.Rotate.left);
        }

        if (coordinate.y == Consts.Coordinates.start)
        {
            rotationAngle.Remove(Consts.Rotate.up);
        }

        if (coordinate.x == column - 1)
        {
            rotationAngle.Remove(Consts.Rotate.right);
        }

        if (coordinate.y == row - 1)
        {
            rotationAngle.Remove(Consts.Rotate.down);
        }
        
        
    }

    private void RotateListClear()
    {
        rotationAngle.Clear();
        rotationAngle.Add(Consts.Rotate.down);
        rotationAngle.Add(Consts.Rotate.right);
        rotationAngle.Add(Consts.Rotate.up);
        rotationAngle.Add(Consts.Rotate.left);
        rotationAngle.Sort();
    }
}