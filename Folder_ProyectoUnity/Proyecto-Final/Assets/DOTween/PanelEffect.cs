using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEffect : MonoBehaviour
{
    public RectTransform panelTransform;
    private Tween panelTween;

    void OnEnable()
    {
        AnimatePanelDrop();
    }

    void AnimatePanelDrop()
    {
        panelTween?.Kill();
        panelTransform.anchoredPosition = new Vector2(0, Screen.height);
        panelTween = panelTransform.DOAnchorPos(Vector2.zero, 1.0f).SetEase(Ease.OutBounce);
    }

    void OnDestroy()
    {
        panelTween?.Kill();
    }
}
