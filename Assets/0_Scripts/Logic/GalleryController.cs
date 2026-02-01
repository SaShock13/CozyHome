using System;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    [SerializeField] private VirtualizedGridGallery gallery;
    [SerializeField] private GalleryClickHandler clickHandler;
    [SerializeField] private GameObject regularPopup;
    [SerializeField] private GameObject premiumPopup;

    private GalleryDataSource _dataSource;

    private void Start()
    {
        // Генерируем тестовые данные
        List<ImageItemData> items = new List<ImageItemData>();
        for (int i = 1; i <= 40; i++)


            items.Add(new ImageItemData(i, $"http://data.ikppbb.com/test-task-unity-data/pics/{i}.jpg", i.ToString()));

        _dataSource = new GalleryDataSource(items);
        gallery.Initialize(_dataSource);
        clickHandler.onImageClicked += OnImageClicked;
    }

    private void OnImageClicked(GalleryItemView view)
    {
        var premium = view.BoundData.IsPremium;

        if (premium)
        {
            premiumPopup.SetActive(true);
            
            regularPopup.SetActive(false);
        }
        else
        {
            regularPopup.SetActive(true);
            premiumPopup.SetActive(false);
            var popupView = regularPopup.GetComponent<PopUpImageView>();
            if(popupView != null)
            {
                popupView.SetData(view.BoundData.LocalName, view.Name);
            }

        }

    }

    public void SetFilter(GalleryFilterType filter)
    {
        _dataSource.ApplyFilter(filter);
        gallery.Refresh();
    }
}
