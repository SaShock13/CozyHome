using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewportSwipeDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform viewport;
    [SerializeField] private float minSwipeDistance = 10;

    public Action onSwipeLeft;
    public Action onSwipeRight;


    private Vector2 startPos;
    private bool isSwiping = false;

    void Start()
    {
        if (viewport == null)
        {
            viewport = GetComponent<RectTransform>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Проверяем что клик внутри Viewport
        if (!RectTransformUtility.RectangleContainsScreenPoint(viewport, eventData.position))
            return;

        startPos = eventData.position;
        isSwiping = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isSwiping) return;

        // Проверяем что отпускание внутри Viewport
        if (!RectTransformUtility.RectangleContainsScreenPoint(viewport, eventData.position))
        {
            isSwiping = false;
            return;
        }

        Vector2 endPos = eventData.position;
        float distance = Vector2.Distance(startPos, endPos);

        if (distance >= minSwipeDistance)
        {
            float horizontalMove = endPos.x - startPos.x;

            if (horizontalMove < 0)
                onSwipeLeft?.Invoke();
            else
                onSwipeRight?.Invoke();
        }

        isSwiping = false;
    }
}