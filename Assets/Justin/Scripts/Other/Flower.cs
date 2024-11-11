using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    
    public bool canBePollinated;

    public float currentValue;
    public ParticleSystem showPollinatedFlower;

    public int honeyAmount;

    private void Awake()
    {
        showPollinatedFlower = GetComponentInChildren<ParticleSystem>();
    }
    public void Pollinate(Slider pollinatingFlower)
    {
        if (canBePollinated)
        {
            currentValue += Time.deltaTime;

            if (pollinatingFlower.value == pollinatingFlower.maxValue)
            {
                PlayerStats.Instance.GetCoins(honeyAmount);
                showPollinatedFlower.Stop();
                DisablePollinating();
            }
        }
        
    }

    public void ShowValueOnSlider(Slider pollinatingSlider)
    {
        pollinatingSlider.value = currentValue;
    }

    public void EnablePollinating()
    {
        canBePollinated = true;
        showPollinatedFlower.Play();
        
    }

    private void DisablePollinating()
    {
        canBePollinated = false;
        FlowerInstantiation.Instance.RemoveFlowerFromArray(transform);
        currentValue = 0;
    }
}
