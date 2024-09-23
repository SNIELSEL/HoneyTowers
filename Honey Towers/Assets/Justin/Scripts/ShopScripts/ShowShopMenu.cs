using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ShowShopMenu : MonoBehaviour
{
    public static ShowShopMenu Instance;

    public GameObject shopCanvas;
    public GameObject normalCanvas;
    public bool isShopping;
    public Transform shopCamera;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void PositionShopCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        Camera.main.transform.position = shopCamera.position;
        Camera.main.transform.rotation = shopCamera.rotation;
        shopCanvas.SetActive(true);
        normalCanvas.SetActive(false);
    }

    private void PositionNormalCamera()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        shopCanvas.SetActive(false);
        normalCanvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PositionShopCamera();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            PositionNormalCamera();
        }
    }
}
