using System.Collections;
using UnityEngine;

public class BannerCarouselAutoScroll : MonoBehaviour
{

    [SerializeField] private BannerCarouselView view;
    [SerializeField] private BannerCarouselController controller;
    [SerializeField] private float scrollDuration = 0.8f;
    [SerializeField] private float pauseDuration = 2f;
    [SerializeField] private AnimationCurve scrollCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine autoScrollRoutine;
    private bool isScrolling;

    public void StartAutoScroll(bool instantly = false)
    {
        StopAutoScroll();
        autoScrollRoutine = StartCoroutine(AutoScrollLoop(instantly));
    }

    public void StopAutoScroll()
    {
        if (autoScrollRoutine != null)
        {
            StopAllCoroutines();
            autoScrollRoutine = null;
            isScrolling = false;
        }
    }

    private IEnumerator AutoScrollLoop(bool instantly)
    {
        if (!instantly)
            yield return new WaitForSeconds(pauseDuration);

        while (true)
        {
            yield return ScrollToNext();
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private IEnumerator ScrollToNext()
    {
        if (isScrolling) yield break;
        isScrolling = true;

        float startPos = view.GetScrollPosition();
        int index = controller.GetNextIndex(view.BannerCount);
        float targetPos = (float)index / (view.BannerCount - 1);

        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            elapsed += Time.deltaTime;
            float t = scrollCurve.Evaluate(elapsed / scrollDuration);
            view.SetScrollPosition(Mathf.Lerp(startPos, targetPos, t));
            yield return null;
        }

        view.SetScrollPosition(targetPos);
        isScrolling = false;
    }
}
