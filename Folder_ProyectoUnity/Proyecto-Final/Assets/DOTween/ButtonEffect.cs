using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Image buttonImage;
    private Color originalColor;
    private Tween scaleTween;
    private Tween colorTween;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            scaleTween?.Kill();
            scaleTween = rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f).SetEase(Ease.OutBounce);
        }

        if (buttonImage != null)
        {
            colorTween?.Kill();
            colorTween = buttonImage.DOColor(Color.yellow, 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            scaleTween?.Kill();
            scaleTween = rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
        }

        if (buttonImage != null)
        {
            colorTween?.Kill();
            colorTween = buttonImage.DOColor(originalColor, 0.3f);
        }
    }

    void OnDestroy()
    {
        scaleTween?.Kill();
        colorTween?.Kill();
    }
}
