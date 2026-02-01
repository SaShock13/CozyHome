using UnityEngine;
using UnityEngine.UI;

public class FadeInController : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Image fadeImage;


    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.CrossFadeAlpha(0f, fadeDuration, false);
    }

}
