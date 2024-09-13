using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShopMenu : MonoBehaviour
{

    public float tweenDuration;
    public float onScreenPos, offScreenPos;
    public RectTransform shopScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopScreen.DOAnchorPosY(onScreenPos, tweenDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            shopScreen.DOAnchorPosY(offScreenPos, tweenDuration);
        }
    }
}
