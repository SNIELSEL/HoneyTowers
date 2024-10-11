using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]
public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public GameObject inventory;
    public GameObject shop;

    public Animator inventoryAnimator;
    public Animator shopAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("E"))
        {
            OpenPanel();
        }

        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha0 + i + 1)))
            {
                inventory.transform.GetChild(i).GetComponent<Button>().onClick.Invoke();

            }
        }

        
    }
    public void OpenPanel()
    {
        if (inventory != null)
        {
            bool isOpen = inventoryAnimator.GetBool("open");
            inventoryAnimator.SetBool("open", !isOpen);
        }
    }
    public void OpenShop()
    {
        if (shop != null)
        {
            bool isOpen = shopAnimator.GetBool("open");
            shopAnimator.SetBool("open", !isOpen);
        }
    }
}
