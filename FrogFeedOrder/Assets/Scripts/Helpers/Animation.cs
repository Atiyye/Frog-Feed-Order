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
    private float tileAnimationDuration = 0.2f;
    private float contentAnimationDuration = 0.1f;
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
            yield return new WaitForSeconds(.3f);

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
        yield return new WaitForSeconds(tiles.Count * Consts.Count.colorCount / 10f);
      
        for (int i = 0; i < tiles.Count; i++)
        {
            contents.Add(Board.Instance.GetLastChild(tiles[i].transform).name);
        }
        
        if (contents.Contains(Consts.Type.arrow))
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
                        yield return new WaitForSeconds(1);
                    }

                    content.DOMove(targetNew.position, 1).OnComplete(() =>
                    {
                        content.DOMove(target.position, 1)
                            .OnComplete(() =>
                            {
                                content.DOScale(0, 1);
                            });
                    });
                    
                }
            }
        }
        else
        {
            for (int i = 0; i < contents.Count; i++)
            {

                Transform target = tiles[0].transform;
                Transform content = Board.Instance.GetLastChild(tiles[i].transform);

                content.DOMove(target.position, 1f)
                    .OnComplete(() =>
                    {
                        content.DOScale(0, .9f);
                        target.DOScale(0, 1);
                    });
            }
        }
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

    public IEnumerator DeleteTileAnimate(Tile tile)
    {
        tile.transform.DOScale(Readonly.ContentValue.contentDelete, tileAnimationDuration)
            .OnComplete(() => { });
        yield return new WaitForSeconds(tileAnimationDuration);
    }

    public IEnumerator CreateContentAnimate(Transform content)
    {
        content.transform.DOScale(Readonly.ContentValue.contentOriginalSize, contentAnimationDuration)
            .OnComplete(() => { });
        yield return new WaitForSeconds(contentAnimationDuration);
    }
}