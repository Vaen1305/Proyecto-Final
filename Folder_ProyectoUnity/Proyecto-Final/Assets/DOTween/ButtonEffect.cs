using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Image buttonImage;
    private Color originalColor;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f).SetEase(Ease.OutBounce);
        buttonImage.DOColor(Color.yellow, 0.3f); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
        buttonImage.DOColor(originalColor, 0.3f);
    }
}
