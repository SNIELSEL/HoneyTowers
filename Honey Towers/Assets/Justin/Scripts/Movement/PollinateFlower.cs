using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollinateFlower : MonoBehaviour
{
    public Canvas canvasSlider;
    public LayerMask flower;

    private void Awake()
    {
        canvasSlider = GetComponentInChildren<Canvas>();
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f, flower))
        {
            if (!hit.transform.GetComponent<Flower>().canBePollinated)
            {
                canvasSlider.enabled = false;
                return;
            }

            canvasSlider.enabled = true;
            if (Input.GetMouseButton(0)) 
            {
                hit.transform.GetComponent<Flower>().Pollinate(canvasSlider.GetComponentInChildren<Slider>());
            }

        }

        else
        {
            canvasSlider.enabled = false;
        }
    }
}
