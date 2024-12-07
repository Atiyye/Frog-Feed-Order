using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelFailedUi : MonoBehaviour
{
    public static LevelFailedUi Instance { get; private set; }
    [SerializeField] private Button newGameButton;
    [SerializeField] private float fadeDuration;
    
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        
        newGameButton.onClick.AddListener(() =>
        {
            ClosePanel();
           
            GameManager.Instance.OnLevelFailed();
            
        });
    }

    public void LevelFailed()
    {
        StartCoroutine(OpenPanel());
    }

    private IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(1);
        canvasGroup.DOFade(1f, fadeDuration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    private void ClosePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
