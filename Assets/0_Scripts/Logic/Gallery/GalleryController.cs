using System;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    [SerializeField] private VirtualizedGridGallery gallery;
    [SerializeField] private GalleryClickHandler clickHandler;
    [SerializeField] private GameObject regularPopup;
    [SerializeField] private GameObject premiumPopup;
    [SerializeField] private PopupAnimation regularPopupAnimated;
    [SerializeField] private PopupAnimation premiumPopupAnimated;

    private GalleryDataSource _dataSource;

    private const string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    private void Start()
    {
        List<ImageItemData> items = new List<ImageItemData>();
        for (int i = 1; i <= 66; i++)


            items.Add(new ImageItemData(i, $"{baseUrl}/{i}.jpg", i.ToString()));

        _dataSource = new GalleryDataSource(items);
        gallery.Initialize(_dataSource);
        clickHandler.onImageClicked += OnImageClicked;
    }

    private void OnImageClicked(GalleryItemView view)
    {
        var premium = view.BoundData.IsPremium;

        if (premium)
        {
            premiumPopupAnimated.Show();
        }
        else
        {
            var popupView = regularPopupAnimated.GetComponentInChildren<PopUpImageView>();
            if(popupView != null)
            {
                regularPopupAnimated.Show();
                popupView.Bind(view.BoundData, view.Name);
            }
        }
    }

    public void SetFilter(GalleryFilterType filter)
    {
        _dataSource.ApplyFilter(filter);
        gallery.Refresh();
    }
}
