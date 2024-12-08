using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animation : MonoBehaviour
{
    public static Animation Instance { get; private set; }

    [Header("Grid")] [SerializeField] private Flexalon.FlexalonGridLayout Grid;

    private List<string> contents = new List<string>();

    private TileGrid grid;

    private float animationDuration = 0.3f;
    private float tileAnimationDuration = 0.3f;
    private float contentAnimationDuration = 0.3f;
    private float contentDeleteDuration = .5f;
    private float moveDuration = 1f;

    private void Awake()
    {
        Instance = this;

        grid = GetComponentInChildren<TileGrid>();
    }

    public IEnumerator ContentsGrowthAnimate(GameObject tileToAnimate)
    {
        List<Tile> tiles = grid.NodeCount(tileToAnimate);

        for (int i = 0; i < tiles.Count; i++)
        {
            Transform AnimObj = tiles[i].transform.GetChild(tiles[i].transform.childCount - 1);
            yield return new WaitForSeconds(Consts.Second.contentsGrowth);

            if (i != tiles.Count - 1)
                TongueCreateAnim(AnimObj);

            if (AnimObj.name != Consts.Type.arrow)
            {
                Anim(AnimObj, Readonly.ContentValue.contentBigSize,
                    Readonly.ContentValue.contentOriginalSize);
            }
        }

        tiles.Clear();
    }

    public IEnumerator ContentGatheringAnimate(GameObject tile)
    {
        List<Tile> tiles = grid.NodeCount(tile);
        yield return new WaitForSeconds(tiles.Count * Consts.Count.colorCount / Consts.Second.contentGathering);

        for (int i = 0; i < tiles.Count; i++)
        {
            contents.Add(Board.Instance.GetLastChild(tiles[i].transform).name);
        }

        #region old animation code

        /*      if (contents.Contains(Consts.Type.arrow))
       {
           int index = contents.IndexOf(Consts.Type.arrow);
           Transform targetNew = tiles[index].transform;

           for (int i = contents.Count - 1; i >= 0; i--)
           {
               Transform target = tiles[0].transform;
               Transform content = Board.Instance.GetLastChild(tiles[i].transform);

               if (contents.Contains(Consts.Type.arrow))
               {
                   if (content.name == Consts.Type.arrow)
                   {
                       targetNew = target;
                       TongueDeleteAnim(content,.01f);
                       yield return new WaitForSeconds(1);
                   }

                   TongueDeleteAnim(content,.01f);
                   content.DOMove(targetNew.position, moveDuration).OnComplete(() =>
                   {
                       content.DOMove(target.position, contentDeleteDuration)
                           .OnComplete(() =>
                           {
                               TongueDeleteAnim(content,.01f);
                               target.DOScale(0, contentDeleteDuration);
                               content.DOScale(0, contentDeleteDuration);
                           });
                   });
               }
           }

           contents.Clear();

       else
      {
        }*/

        #endregion

 
            for (int i = 0; i < tiles.Count; i++)
            {
                Transform target = tiles[0].transform;
                Transform content = Board.Instance.GetLastChild(tiles[i].transform);
                TongueDeleteAnim(content,Consts.Second.tongueDelete);
                content.DOMove(target.position, moveDuration)
                    .OnComplete(() =>
                    {
                        target.DOScale(0, contentDeleteDuration);
                        content.DOScale(0, contentDeleteDuration);
                    });
            }

            contents.Clear();
        //}
    }

    private void Anim(Transform animObj, Vector3 startValue, Vector3 endValue)
    {
        animObj.DOScale(startValue, animationDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => { animObj.DOScale(endValue, animationDuration); });
    }

    private void TongueCreateAnim(Transform content)
    {
        if (content.name == Consts.Type.arrow)
        {
            Tongue.Instance.direction = int.Parse(content.transform.localRotation.eulerAngles.y.ToString());
            if (Tongue.Instance.direction == Consts.Rotate.left
                || Tongue.Instance.direction == Consts.Rotate.right)
                Tongue.Instance.direction = (Consts.Rotate.left + Tongue.Instance.direction) % 360;
            else
                Tongue.Instance.direction = (Consts.Rotate.left - Tongue.Instance.direction) % 360;
        }

        Transform tongue = content.GetChild(content.childCount - 1);

        Tongue.Instance.UpdateTongue(content.name, content);

        if (content.name != Consts.Type.arrow)
            Anim(tongue, Readonly.TongueValue.tongueDelete,
                Readonly.TongueValue.tongueCreate);
        else
            Anim(tongue, Readonly.TongueValue.tongueDelete,
                Readonly.TongueValue.tongueCreateArrow);
    }

    public void TongueDeleteAnim(Transform content,float duration)
    {   
        Transform tongue = content.GetChild(content.childCount - 1);
        tongue.DOScale(Readonly.TongueValue.tongueDelete, duration);
    }
}