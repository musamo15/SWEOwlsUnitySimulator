using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectText;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        objectText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        objectText.SetActive(false);
    }
}