using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLevel : MonoBehaviour
{
    [Header("Scrip Reverenses")]
    private BaseTurret baseTurret;
    
    
    [Header("Levels")]

    private int currentLevel;

    [SerializeField] private TMP_Text levelText;


    private void Awake()
    {
        baseTurret = gameObject.GetComponent<BaseTurret>();
    }


    private void Update()
    {
        ShowCurrentLevel();
    }



    private void ShowCurrentLevel()
    {
        currentLevel = baseTurret.timesUpgraded + 1;


        levelText.text = "Level " + currentLevel.ToString();
    }
}
