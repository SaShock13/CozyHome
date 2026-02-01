using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpImageView : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] TMP_Text text;
    private RectTransform rectTransform;

    public void SetData(string localName,string imageName)
    {
        image.texture = ImageLoaderService.LoadResource(localName);
        rectTransform = image.GetComponent<RectTransform>();
        AdjustHeightToTextureAspect();
        text.text = imageName;

    }
    void AdjustHeightToTextureAspect()
    {
        if (image.texture != null)
        {
            float aspectRatio = (float)image.texture.height / image.texture.width;
            float newHeight = rectTransform.rect.width * aspectRatio;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
    }
}
