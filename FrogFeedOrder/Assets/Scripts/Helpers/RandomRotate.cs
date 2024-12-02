using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotate : MonoBehaviour
{
    [SerializeField] public List<int> rotationAngle;

    [SerializeField] public int direction;

    public void ContentRotation(GameObject content, String contentType, Vector3Int coordinate,uint column,uint row,int nodeRotation)
    {
        if (contentType == Consts.Type.frog)
        {
            RandomRotation(coordinate, column, row, contentType,nodeRotation);
            content.transform.localRotation = Quaternion.Euler(0, rotationAngle[CreateRotate()], 0);
            direction = int.Parse(content.transform.localRotation.eulerAngles.y.ToString());
        }
        else if (contentType == Consts.Type.arrow)
        {
            RandomRotation(coordinate, column, row, contentType,nodeRotation);
            content.transform.localRotation =
                Quaternion.Euler(content.transform.localRotation.eulerAngles.x, rotationAngle[CreateRotate()], 0);
            direction =  int.Parse(content.transform.localRotation.eulerAngles.y.ToString());
            direction = (Consts.Rotate.left + direction) % 360;
        }
        else
        {
            content.transform.localRotation = Quaternion.Euler(0, Consts.Rotate.up, 0);
            direction = direction;
        }
    }
    
    private void RandomRotation(Vector3Int coordinate,uint column,uint row,string type,int nodeDirection)
    {
        RotateListClear();
        CoordinateControl(coordinate, column, row, type,nodeDirection);
    }

    private int CreateRotate()
    {
        int rotation = Random.Range(0, rotationAngle.Count);
        return rotation;
    }

     private void CoordinateControl(Vector3Int coordinate,uint column,uint row,string type,int nodeDirection)
    {
        if (type == Consts.Type.frog)
        {
            if (coordinate.x == Consts.Coordinates.start)
                rotationAngle.Remove(Consts.Rotate.left);

            if (coordinate.y == Consts.Coordinates.start)
                rotationAngle.Remove(Consts.Rotate.up);

            if (coordinate.x == column - 1)
                rotationAngle.Remove(Consts.Rotate.right);

            if (coordinate.y == row - 1)
                rotationAngle.Remove(Consts.Rotate.down);
        }
        
        else if (type == Consts.Type.arrow)
        {
            if (coordinate.x == Consts.Coordinates.start)
                rotationAngle.Remove(Consts.Rotate.down);

            if (coordinate.y == Consts.Coordinates.start)
                rotationAngle.Remove(Consts.Rotate.left);

            if (coordinate.x == column - 1)
                rotationAngle.Remove(Consts.Rotate.up);

            if (coordinate.y == row - 1)
                rotationAngle.Remove(Consts.Rotate.right);
            
            rotationAngle.Remove((nodeDirection + Consts.Rotate.left) % 360);
        }
    }

    private void RotateListClear()
    {
        rotationAngle.Clear();
        rotationAngle.Add(Consts.Rotate.down);
        rotationAngle.Add(Consts.Rotate.right);
        rotationAngle.Add(Consts.Rotate.up);
        rotationAngle.Add(Consts.Rotate.left);
    }

  
}
