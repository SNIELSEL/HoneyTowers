using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject panel;



    public void Update()
    {
        if(Input.GetButtonDown("E"))
        {
            OpenPanel();
        }
    }
    public void OpenPanel()
    {
        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null )
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }
}
