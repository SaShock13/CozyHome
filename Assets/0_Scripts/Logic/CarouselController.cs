using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{   
    [SerializeField] BannerScroll banerSroller;
    [SerializeField] private List<Image> dotImages;
    [SerializeField] private Color highlightetColor = Color.green;
    [SerializeField] private Color regularColor = Color.white;

    private void Start()
    {
        banerSroller.onBannerChanged += OnBanerChanged;
    }

    private void OnBanerChanged(int index)
    {
        foreach (var image in dotImages)
        {
            image.color = regularColor; ;
        }
        dotImages[index].color = highlightetColor;
    }
}
