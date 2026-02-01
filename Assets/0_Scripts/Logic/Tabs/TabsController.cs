using UnityEngine;
using UnityEngine.UI;

public class TabsController : MonoBehaviour
{
    [SerializeField] private Button tabAll;
    [SerializeField] private Button tabOdd;
    [SerializeField] private Button tabEven;

    [SerializeField] private GalleryController galleryController;

    private void Start()
    {
        tabAll.onClick.AddListener(() => galleryController.SetFilter(GalleryFilterType.All));
        tabOdd.onClick.AddListener(() => galleryController.SetFilter(GalleryFilterType.Odd));
        tabEven.onClick.AddListener(() => galleryController.SetFilter(GalleryFilterType.Even));
    }
}
