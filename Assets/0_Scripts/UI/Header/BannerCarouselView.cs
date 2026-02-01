using UnityEngine;
using UnityEngine.UI;

public class BannerCarouselView : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;

    private RectTransform[] banners;
    private float viewportWidth;

    public int BannerCount => banners.Length;

    public void Initialize()
    {
        banners = GetChildrenArray();
        viewportWidth = scrollRect.viewport.rect.width;

        for (int i = 0; i < banners.Length; i++)
        {
            banners[i].sizeDelta = new Vector2(viewportWidth, banners[i].sizeDelta.y);
        }

        SetupContent();
        SetupBannerPositions();
        scrollRect.horizontalNormalizedPosition = 0;
    }

    public float GetScrollPosition()
    {
        return scrollRect.horizontalNormalizedPosition;
    }

    public void SetScrollPosition(float position)
    {
        scrollRect.horizontalNormalizedPosition = position;
    }

    private void SetupContent()
    {
        content.sizeDelta = new Vector2(BannerCount * viewportWidth, content.sizeDelta.y);
        scrollRect.inertia = false;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
    }

    private void SetupBannerPositions()
    {
        for (int i = 0; i < banners.Length; i++)
        {
            banners[i].anchorMin = new Vector2(0, 0.5f);
            banners[i].anchorMax = new Vector2(0, 0.5f);
            banners[i].pivot = new Vector2(0, 0.5f);
            banners[i].anchoredPosition = new Vector2(i * viewportWidth, 0);
        }
    }

    private RectTransform[] GetChildrenArray()
    {
        RectTransform[] rects = new RectTransform[content.childCount];
        for (int i = 0; i < content.childCount; i++)
            rects[i] = content.GetChild(i).GetComponent<RectTransform>();

        return rects;
    }
}
