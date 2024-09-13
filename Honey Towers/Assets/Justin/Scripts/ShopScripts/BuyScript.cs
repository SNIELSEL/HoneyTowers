using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public InventoryScript inventoryScript;
    public int price;

    public void Buy()
    {
        if (!PlayerStats.Instance.LoseCoins(price)) return;

        inventoryScript.AmountGoesUp();
    }
}
