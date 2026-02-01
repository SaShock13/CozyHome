using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BannerScroll : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private ViewportSwipeDetector swipeDetector;

    [Header("Настройки")]
    [SerializeField] private float scrollDuration = 0.8f;
    [SerializeField] private float pauseDuration = 2f;
    [SerializeField] private AnimationCurve scrollCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float viewportWidth;
    private int currentIndex = 0;
    private bool isScrolling = false;
    private bool movingRight = true;
    private int bannersCount ;

    private RectTransform[] banners ;
    private Coroutine autoScrollCoroutine;

    public Action<int> onBannerChanged;

    void Start()
    {
        Initialize();
        StartAutoScroll();
        swipeDetector.onSwipeLeft += OnSwipeLeft;
        swipeDetector.onSwipeRight += OnSwipeRight;
    }

    void Initialize()
    {
        banners = GetChildrenArray();
        bannersCount = banners.Length;
        viewportWidth = scrollRect.viewport.rect.width;

        // Настраиваем размеры баннеров
        for (int i = 0; i < bannersCount; i++)
        {
            banners[i].sizeDelta = new Vector2(viewportWidth, banners[i].sizeDelta.y);
        }

        SetupContent();
        SetupBannerPositions();

        // начальная позиция
        scrollRect.horizontalNormalizedPosition = 0;
    }

    public RectTransform[] GetChildrenArray()
    {
        if (content == null) return new RectTransform[0];

        RectTransform[] rects = new RectTransform[content.childCount];
        for (int i = 0; i < content.childCount; i++)
        {
            rects[i] = content.GetChild(i).GetComponent<RectTransform>();
        }
        return rects;
    }

    void SetupContent()
    {
        // Ширина Content = BannerCount * ширина Viewport
        float totalWidth = bannersCount * viewportWidth;
        content.sizeDelta = new Vector2(totalWidth, content.sizeDelta.y);

        scrollRect.inertia = false;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
    }

    void SetupBannerPositions()
    {
        for (int i = 0; i < bannersCount; i++)
        {
            // выставляю якоря
            banners[i].anchorMin = new Vector2(0, 0.5f);
            banners[i].anchorMax = new Vector2(0, 0.5f);
            banners[i].pivot = new Vector2(0, 0.5f);

            // Позиция
            float xPos = i * viewportWidth;
            banners[i].anchoredPosition = new Vector2(xPos, 0);
        }
    }

    void StartAutoScroll(bool instantly = false)
    {
        if (autoScrollCoroutine != null)
            StopCoroutine(autoScrollCoroutine);

        autoScrollCoroutine = StartCoroutine(AutoScrollLoop(instantly));
    }
    public void PauseAutoScroll()
    {
        if (autoScrollCoroutine != null)
        {
            StopCoroutine(autoScrollCoroutine);
            autoScrollCoroutine = null;
        }
    }

    IEnumerator AutoScrollLoop(bool instantly)
    {
        // Пауза перед скроллом
        if(!instantly) yield return new WaitForSeconds(pauseDuration);

        while (true)
        {
            // Скролл к следующему баннеру
            yield return StartCoroutine(ScrollToNext());
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator ScrollToNext()
    {
        if (isScrolling) yield break;

        isScrolling = true;

        // Обновляем индекс
        if (movingRight)
        {
            currentIndex = (currentIndex + 1) ;
        }
        else
        {
            currentIndex = (currentIndex - 1) ;
        }
        currentIndex = Mathf.Clamp(currentIndex, 0, bannersCount - 1);

        onBannerChanged?.Invoke(currentIndex);

        // считаем целевую позицию
        float startPos = scrollRect.horizontalNormalizedPosition;
        float targetPos = (float)currentIndex / (float)(bannersCount - 1); // 0,1/3, 2/3, 1

        // Анимация скролла
        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scrollDuration;
            t = scrollCurve.Evaluate(t);

            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // Финализация
        scrollRect.horizontalNormalizedPosition = targetPos;


        // Если двигается вправо и уже в конце, идем налево
        if (movingRight && currentIndex == bannersCount-1)
        {
            movingRight = false;
        }

        // Если двигается вдево и уже в начале, идем направо    
        if (!movingRight && currentIndex == 0)
        {
            movingRight = true;
        }

        isScrolling = false;
    }

    public void OnSwipeRight()
    {

        Debug.Log($"OnSwipeRight {this}");
        PauseAutoScroll();
        movingRight = false;
        StartAutoScroll(true);
    }

    public void OnSwipeLeft()
    {
        Debug.Log($"OnSwipeLeft {this}");
        PauseAutoScroll();
        movingRight = true;
        StartAutoScroll(true);
    }


    void OnDestroy()
    {
        if (autoScrollCoroutine != null)
            StopCoroutine(autoScrollCoroutine);
    }
}