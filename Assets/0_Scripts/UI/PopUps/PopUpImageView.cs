using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpImageView : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] TMP_Text text;

    private Texture placeHolderTexture;
    private RectTransform rectTransform;

    private void Start()
    {
        placeHolderTexture = image.texture;
    }

    public void Bind(ImageItemData data, string imageName)
    {
        Reset();
        if (data.Texture != null) { SetTexture(data.Texture); }
        else StartCoroutine(LoadImageAsync(data.ImageUrl));        
        rectTransform = image.GetComponent<RectTransform>();
        AdjustHeightToTextureAspect();
        text.text = imageName;

    }

    private void Reset()
    {
        SetTexture(placeHolderTexture);
    }

    private IEnumerator LoadImageAsync(string imageUrl)
    {
        Debug.Log($"LOading  {imageUrl}");
        yield return ImageLoaderService.LoadFromNet(imageUrl, tex =>
        {
            if (tex != null)
            {
                SetTexture(tex);
            }
            else Debug.Log($"Can not load texture from  {imageUrl}");
        });
    }

    private void SetTexture(Texture texture)
    {
        image.texture = texture;
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
