using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GalleryItemView : MonoBehaviour
{
    public event Action<ImageItemData> OnClick;

    [SerializeField] private RawImage image;
    [SerializeField] private GameObject premiumBadge;
    [SerializeField] private TMP_Text nameText;

    public string Name { get; set; }

    public int BoundIndex { get; private set; } = -1;
    public ImageItemData BoundData { get; private set; }

    private Coroutine loadingCoroutine;

    public RectTransform RectTransform => (RectTransform)transform;

    public void Bind(ImageItemData data, int index)
    {
        BoundIndex = index;
        BoundData = data;
        Name = $"Изображение {data.Id}";
        if (premiumBadge != null) premiumBadge.SetActive(data.IsPremium);
        if (nameText != null) nameText.text = Name;

        if (data.Texture != null)
        {
            image.texture = data.Texture;
        }
        else
        {
            image.texture = null;

            //image.texture = ImageLoaderService.LoadResource(data.LocalName);
            if (loadingCoroutine != null) StopCoroutine(loadingCoroutine);
            loadingCoroutine = StartCoroutine(LoadImageAsync(data));
        }

        gameObject.SetActive(true);
    }

    private IEnumerator LoadImageAsync(ImageItemData data)
    {
        yield return ImageLoaderService.LoadResourceAsync(data.LocalName, tex =>
        {
            if (tex != null)
            {
                data.Texture = tex;
                image.texture = tex;
            }
        });

        loadingCoroutine = null;
    }

    public void Unbind()
    {
        BoundIndex = -1;
        BoundData = null;
        image.texture = null;

        if (loadingCoroutine != null)
        {
            StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
        }
    }

    public void OnClickHandler()
    {
        OnClick?.Invoke(BoundData);
    }

}
