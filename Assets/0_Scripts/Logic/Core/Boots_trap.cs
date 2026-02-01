using UnityEngine;

public class Bootstrap : MonoBehaviour // На всякий случай  
{
    [SerializeField] private TabsController tabsController;
    [SerializeField] private GalleryController galleryController;

    private void Start()
    {
        // ИНИЦИАЛИЗАЦИЯ И ПРОБРОС ЗАВИСИМОСТЕЙ
    }
}
