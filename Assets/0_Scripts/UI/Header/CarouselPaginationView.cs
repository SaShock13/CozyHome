using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselPaginationView : MonoBehaviour
{
    [SerializeField] private List<Image> dots;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.white;

    public void SetActive(int index)
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].color = i == index ? activeColor : inactiveColor;
        }
    }
}
