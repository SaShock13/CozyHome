using UnityEngine;

[ExecuteAlways] 
public class UISafeArea : MonoBehaviour
{
    [SerializeField] private bool applyOnAwake = true;
    [SerializeField] private bool applyTop = true;
    [SerializeField] private bool applyBottom = true;
    [SerializeField] private bool applyLeft = true;
    [SerializeField] private bool applyRight = true;

    private RectTransform rectTransform;
    private Rect lastSafeArea;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (applyOnAwake)
            ApplySafeArea();
    }

    void Update()
    {
        // Обновление при изменении safe area
        if (lastSafeArea != Screen.safeArea)
            ApplySafeArea();
    }

    void ApplySafeArea()
    {
        lastSafeArea = Screen.safeArea;

        Vector2 anchorMin = lastSafeArea.position;
        Vector2 anchorMax = lastSafeArea.position + lastSafeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        if (!applyLeft) anchorMin.x = 0;
        if (!applyRight) anchorMax.x = 1;
        if (!applyBottom) anchorMin.y = 0;
        if (!applyTop) anchorMax.y = 1;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }
#endif
}