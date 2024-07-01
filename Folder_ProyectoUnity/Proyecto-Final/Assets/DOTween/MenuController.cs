using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image titleImage;
    [SerializeField] private Button[] buttons;
    [SerializeField] private float animationDuration = 3f; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Inicio")
        {
            AnimateTitle();
            AnimateButtons();
        }
    }

    private void AnimateTitle()
    {
        if (titleImage != null)
        {
            titleImage.rectTransform.DOKill();

            titleImage.rectTransform.DOAnchorPosY(20, 1f).SetEase(Ease.InOutQuad).SetLoops(6, LoopType.Yoyo);
        }
    }

    private void AnimateButtons()
    {
        if (buttons != null && buttons.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != null)
                {
                    buttons[i].transform.DOKill();

                    buttons[i].transform.DOScale(1.1f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(6, LoopType.Yoyo);
                }
            }
        }
    }
}
