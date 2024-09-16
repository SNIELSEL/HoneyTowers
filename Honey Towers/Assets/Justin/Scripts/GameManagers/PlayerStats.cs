using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int coins;
    public TMP_Text coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }  
    

    public bool LoseCoins(int price)
    {
        if (coins >= price)
        {
            coins -= price;
            coinText.text = "Coins: " + coins.ToString();
            return true;
        }

        return false;
    }

    public void GetCoins(int coinsCollected)
    {
        coins += coinsCollected;
        coinText.text = "Coins: " + coins.ToString();
    }

    
}
