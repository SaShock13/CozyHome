using System;
using UnityEngine;

public class TabsView : MonoBehaviour
{    

    public event Action<GalleryFilterType> OnTabSelected;

    [SerializeField] private TabButtonView allTab;
    [SerializeField] private TabButtonView oddTab;
    [SerializeField] private TabButtonView evenTab;

    private void Awake()
    {
        allTab.Init(GalleryFilterType.All, OnTabClicked);
        oddTab.Init(GalleryFilterType.Odd, OnTabClicked);
        evenTab.Init(GalleryFilterType.Even, OnTabClicked);
    }

    private void OnTabClicked(GalleryFilterType type)
    {
        SetActiveTab(type);
        OnTabSelected?.Invoke(type);
    }

    public void SetActiveTab(GalleryFilterType activeType)
    {
        allTab.SetActive(activeType == GalleryFilterType.All);
        oddTab.SetActive(activeType == GalleryFilterType.Odd);
        evenTab.SetActive(activeType == GalleryFilterType.Even);
    }
}
