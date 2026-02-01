using System;
using UnityEngine;

public class BannerCarouselController : MonoBehaviour
{
    public Action<int> OnBannerChanged;

    private int currentIndex;
    private bool movingRight = true;

    public int GetNextIndex(int bannerCount)
    {
        currentIndex += movingRight ? 1 : -1;
        currentIndex = Mathf.Clamp(currentIndex, 0, bannerCount - 1);

        if (currentIndex == bannerCount - 1)
            movingRight = false;
        else if (currentIndex == 0)
            movingRight = true;

        OnBannerChanged?.Invoke(currentIndex);
        return currentIndex;
    }

    public void SetDirection(bool toRight)
    {
        movingRight = toRight;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
