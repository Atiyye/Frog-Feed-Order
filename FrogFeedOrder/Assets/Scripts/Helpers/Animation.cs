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
                Tongue.Instance.direction = int.Parse(AnimObj.transform.localRotation.eulerAngles.y.ToString());
                Tongue.Instance.direction = (Consts.Rotate.left - Tongue.Instance.direction) % 360;
            }
            
            if (i != tiles.Count - 1)
                TongueAnim(AnimObj);
            
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

    private void TongueAnim(Transform content)
    {
        Transform tongue = content.GetChild(content.childCount - 1);
   
        Tongue.Instance.UpdateTongue(content.name,content);

        if (content.name != Consts.Type.arrow)
            Anim(tongue, new Vector3(0f, 0f, 0f),
                new Vector3(22f, 5f,2f), Consts.FunctionType.create);
        else
            Anim(tongue, new Vector3(0f, 0f, 0f), 
                new Vector3(1f, .2f, .2f), Consts.FunctionType.create);
        
    }

    private void Anim(Transform animObj,Vector3 startValue ,Vector3 endValue , string functionType)
    {
        animObj.DOScale(startValue, animationDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                animObj.DOScale(endValue, animationDuration);
            });
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
