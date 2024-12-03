using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
   public Button frogBtn;
   public int direction;
   private GameObject tile;

   private void Awake()
   {
      direction = int.Parse(gameObject.transform.localRotation.eulerAngles.y.ToString());
      tile = gameObject.transform.parent.gameObject;
      
      frogBtn.onClick.AddListener(() =>
      {
         Board.Instance.FeedFrog(direction,tile);
      });
   }
}
