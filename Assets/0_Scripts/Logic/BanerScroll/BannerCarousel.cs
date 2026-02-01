using UnityEngine;

public class BannerCarousel : MonoBehaviour
{

    [SerializeField] private BannerCarouselView view;
    [SerializeField] private BannerCarouselController controller;
    [SerializeField] private BannerCarouselAutoScroll autoScroll;
    [SerializeField] private ViewportSwipeDetector swipeDetector;
    [SerializeField] private CarouselPaginationView paginationView;

    private void Start()
    {
        view.Initialize();
        autoScroll.StartAutoScroll();

        swipeDetector.onSwipeLeft += OnSwipeLeft;
        swipeDetector.onSwipeRight += OnSwipeRight;
        controller.OnBannerChanged += paginationView.SetActive;

    }

    private void OnSwipeLeft()
    {
        autoScroll.StopAutoScroll();
        controller.SetDirection(true);
        autoScroll.StartAutoScroll(true);
    }

    private void OnSwipeRight()
    {
        autoScroll.StopAutoScroll();
        controller.SetDirection(false);
        autoScroll.StartAutoScroll(true);
    }

    private void OnDestroy()
    {
        swipeDetector.onSwipeLeft -= OnSwipeLeft;
        swipeDetector.onSwipeRight -= OnSwipeRight;
        controller.OnBannerChanged -= paginationView.SetActive;
    }
}
