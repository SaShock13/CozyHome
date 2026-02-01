using System;
using UnityEngine;
using UnityEngine.UI;

public class TabButtonView : MonoBehaviour
{  

    [SerializeField] private Button button;
    [SerializeField] private GameObject activeIndicator;

    private GalleryFilterType filterType;
    private Action<GalleryFilterType> callback;

    public void Init(GalleryFilterType type, Action<GalleryFilterType> onClick)
    {
        filterType = type;
        callback = onClick;
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        callback?.Invoke(filterType);
    }

    public void SetActive(bool isActive)
    {
        if (activeIndicator == null) return; 
        activeIndicator.SetActive(isActive);
    }
}
