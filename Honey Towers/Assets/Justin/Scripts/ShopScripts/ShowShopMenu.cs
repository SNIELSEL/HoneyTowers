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

    public float cameraMoveSpeed;


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
        StartCoroutine(MoveTheCamera());
        Camera.main.transform.rotation = shopCamera.rotation;
        shopCanvas.SetActive(true);
        normalCanvas.SetActive(false);
    }

    private IEnumerator MoveTheCamera()
    {
        while (Camera.main.transform.position != shopCamera.position && isShopping)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, shopCamera.position, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void PositionNormalCamera()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        Camera.main.GetComponent<CameraMovement>().isMovingCamera = false;
        shopCanvas.SetActive(false);
        normalCanvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Renderer renderer in other.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
            isShopping = true;
            PositionShopCamera();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Renderer renderer in other.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
            isShopping = false;
            PositionNormalCamera();
        }
    }
}
