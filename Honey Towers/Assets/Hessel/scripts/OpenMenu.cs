using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject menu;
    public void Update()
    {
        if (Input.GetButtonDown("esc"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }
    public void CloseTheMenu()
    {
        Time.timeScale = 1;
    }
}

