using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject menu;
    public bool isOpen;
    public void Update()
    {
        if (Input.GetButtonDown("esc") && !isOpen)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            isOpen = true;
        }
    }
    public void CloseTheMenu()
    {
        Time.timeScale = 1;
        isOpen = false;

    }
}

