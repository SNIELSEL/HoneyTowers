using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public TurretInfo turret;
    public void ChangingTurret()
    {
        DroppingTurretScript.Instance.TurretChange(turret);
    }

    public void AmountGoesUp()
    {
        turret.turretAmount++;
        turret.turretAmountText.text = turret.turretAmount.ToString();
    }

    public void SelectedColor(Image selectedImage)
    {
        Image[] images = transform.parent.GetComponentsInChildren<Image>();


        foreach (Image image in images)
        {
            if (image != transform.parent.GetComponent<Image>())
            {
                if (image == selectedImage)
                {
                    image.color = Color.red;
                }
                else
                {
                    image.color = Color.white;
                }
            }
            
            
        }
    }
}
