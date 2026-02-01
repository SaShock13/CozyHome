using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private TabsController tabsController;
    [SerializeField] private GalleryController galleryController;
    [SerializeField] private CarouselController carouselController;

    private void Start()
    {
        // ИНИЦИАЛИЗАЦИЯ И ПРОБРОС ЗАВИСИМОСТЕЙ
    }
}
