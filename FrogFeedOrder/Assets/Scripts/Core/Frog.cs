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
         int moves = GameManager.Instance.moves;

         if (moves > 0)
         {
            GameManager.Instance.moves--;
            GameUi.Instance.UpdateMovesText(GameManager.Instance.moves);
            Board.Instance.FeedFrog(direction,tile);
         }
      });
   }
   
}
