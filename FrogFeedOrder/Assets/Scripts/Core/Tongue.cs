using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public static Tongue Instance { get;  set; }
    public int direction;
    
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateTongue(string parentName, Transform content)
    {
        Transform tongue = content.GetChild(content.childCount - 1);
        
        switch (parentName)
        {
            case Consts.Type.grape:
                tongue.transform.localRotation = Quaternion.Euler(
                    tongue.transform.localRotation.eulerAngles.x,
                    direction + Consts.Rotate.left,
                    tongue.transform.localRotation.eulerAngles.z
                );
                break;
        }
    }
}
