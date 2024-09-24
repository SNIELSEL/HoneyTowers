using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void PlayMap1()
    {
        SceneManager.LoadScene("Map1");
    }
    public void PlayMap2()
    {
        SceneManager.LoadScene("Map2");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
