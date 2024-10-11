using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventory;
    public GameObject shop;


    public void Update()
    {
        if(Input.GetButtonDown("E"))
        {
            OpenPanel();
        }
        if (Input.GetButtonDown("Z"))
        {
            OpenShop();
        }
    }
    public void OpenPanel()
    {
        if (inventory != null)
        {   
            var animator = inventory.GetComponent<Animator>();
            if (animator != null )
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }
    public void OpenShop()
    {
        if (shop != null)
        {
            var animator = shop.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }
}
