using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

public class PopupAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup background;
    [SerializeField] private CanvasGroup popup;
    [SerializeField] private RectTransform popupPanel;

    [Header("Animation")]
    [SerializeField] private float fadeDuration = 0.2f;
    [SerializeField] private float scaleDuration = 0.25f;
    [SerializeField] private float backgroundAlpha = 0.6f;

    private Coroutine animationCoroutine;

    private void Awake()
    {

        ResetState();
    }

    public void Show()
    {
        background.gameObject.SetActive(true);
        gameObject.SetActive(true);
        ResetState();

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        background.blocksRaycasts = true;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / fadeDuration;
            background.alpha = Mathf.Lerp(0f, backgroundAlpha, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / scaleDuration;
            float eased = EaseOutBack(t);
            popupPanel.localScale = Vector3.one * eased;
            yield return null;
        }

        popupPanel.localScale = Vector3.one;
    }

    private IEnumerator HideRoutine()
    {
        float t = 0f;

        Vector3 startScale = popupPanel.localScale;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / scaleDuration;
            popupPanel.localScale = Vector3.Lerp(startScale, Vector3.one * 0.2f, t);
            background.alpha = Mathf.Lerp(backgroundAlpha, 0f, t);
            popup.alpha = Mathf.Lerp(1, 0f, t);
            yield return null;
        }

        ResetState();
        background.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }

    private void ResetState()
    {
        background.alpha = 0f;
        
        popup.alpha = 1f;
        background.blocksRaycasts = false;
        popupPanel.localScale = Vector3.zero ;
    }

    // easing 
    private float EaseOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
}
