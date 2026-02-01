using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashController : MonoBehaviour
{
    [SerializeField] private Image splashImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayTime = 2f;
    [SerializeField] private string nextScene = "Menu";

    private void Start()
    {
        StartCoroutine(SplashSequence());
    }

    private IEnumerator SplashSequence()
    {
        // Fadein
        splashImage.canvasRenderer.SetAlpha(0f);
        splashImage.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration + displayTime);

        // Fadeout
        splashImage.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // Переход на главную сцену
        SceneManager.LoadScene(nextScene);
    }
}
