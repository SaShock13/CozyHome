using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClickHandler : MonoBehaviour, IPointerClickHandler
{
    private PopupAnimation animation;

    private void Start()
    {
        animation = GetComponentInParent<PopupAnimation>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log($"Clicked {this}");
        animation.Hide();
    }
}
