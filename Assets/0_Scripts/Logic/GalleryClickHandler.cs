using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GalleryClickHandler : MonoBehaviour,IPointerClickHandler
{

    public System.Action<GalleryItemView> onImageClicked;
    public void OnPointerClick(PointerEventData eventData)  // todo переделать, сейчас даже через попааы пробивает
    {

        // Получаем ВСЕ объекты под курсором
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // Ищем GalleryItemView в любом месте иерархии
        GalleryItemView clickedView = null;

        foreach (var result in results)
        {
            // Пробуем найти в текущем объекте
            clickedView = result.gameObject.GetComponent<GalleryItemView>();
            if (clickedView != null) break;

            // Пробуем найти в родителях
            clickedView = result.gameObject.GetComponentInParent<GalleryItemView>();
            if (clickedView != null) break;
        }

        if (clickedView != null)
        {
            OnGalleryItemClicked(clickedView);
        }
        else
        {
            Debug.LogWarning("Не удалось найти GalleryItemView!");

            // Выводим отладочную информацию
            for (int i = 0; i < results.Count; i++)
            {
                Debug.Log($"[{i}] {results[i].gameObject.name}");
            }
        }
    }

    private void OnGalleryItemClicked(GalleryItemView clickedView)
    {
        if (clickedView.BoundData.IsPremium) Debug.Log($"{clickedView.Name} IS PREMIUM IMAGE");
        else Debug.Log($"{clickedView.Name} regular image ");
        onImageClicked.Invoke(clickedView);

    }
}
