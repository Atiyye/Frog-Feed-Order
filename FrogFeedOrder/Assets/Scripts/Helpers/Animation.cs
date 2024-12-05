using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animation : MonoBehaviour
{    
    public static Animation Instance { get; private set; }
    
    [Header("Grid")]
    [SerializeField] private Flexalon.FlexalonGridLayout Grid;
    private TileGrid grid;
    
    private float animationDuration = 0.3f;
    private float tileAnimationDuration = 0.2f;
    private float contentAnimationDuration = 0.1f;
    private float moveDuration  = 1f;
    
    private void Awake()
    {
        Instance = this;
        
        grid = GetComponentInChildren<TileGrid>();
    }
    
    public IEnumerator ContentsGrowthAnimate(GameObject tileToAnimate, int nodeDirection)
    {
        List<Tile> tiles = grid.NodeCount(tileToAnimate);
        
        for (int i = 0; i < tiles.Count; i++)
        {
            Transform AnimObj = tiles[i].transform.GetChild(tiles[i].transform.childCount - 1);
            yield return new WaitForSeconds(.3f);
            
            if (AnimObj.name == Consts.Type.arrow)
            {
                nodeDirection = (nodeDirection + Consts.Rotate.left) % 360;
            }
            
            if (i != tiles.Count - 1)
                StickOutTongue(AnimObj, nodeDirection);
            if (AnimObj.name != Consts.Type.arrow)
            {
                AnimObj.DOScale(1f, animationDuration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        AnimObj.DOScale(.75f, animationDuration);
                    });
            }
        }
        tiles.Clear();
    }

    private void StickOutTongue(Transform content,int nodeDirection)
    {
        Transform tongue = content.GetChild(content.childCount - 1);
       
        
       if (content.name == Consts.Type.grape)
       {
           tongue.transform.localRotation =
               Quaternion.Euler(Consts.Rotate.left, nodeDirection % 360, 0);
       }

        if (content.name != Consts.Type.arrow)
        {
            tongue.DOScale(0, animationDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    tongue.gameObject.SetActive(true);
                    tongue.DOScale(new Vector3(22f, 5f,2f), animationDuration);
                });
        }
        else
        {
            tongue.DOScale(0, animationDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    tongue.gameObject.SetActive(true);
                    tongue.DOScale(new Vector3(1f,.2f,.2f), animationDuration);
                });
        }
        
    }
    
    public IEnumerator DeleteTileAnimate(Tile tile)
    {
        tile.transform.DOScale(0f, tileAnimationDuration)
            .OnComplete(() => {});
        yield return new WaitForSeconds(tileAnimationDuration);
    }
    
    public IEnumerator CreateContentAnimate(Transform content)
    {
        content.transform.DOScale(0.75f, contentAnimationDuration)
            .OnComplete(() => {});
        yield return new WaitForSeconds(contentAnimationDuration);
    }
}
