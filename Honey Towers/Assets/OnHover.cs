using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color color;
    public Image colorReference;
    private void Awake()
    {
        color = GetComponentInChildren<TMP_Text>().color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = colorReference.color;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = color;
    }


}
