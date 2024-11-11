using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellScreen : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.OpenShop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.OpenShop();
        }
    }
    
}
