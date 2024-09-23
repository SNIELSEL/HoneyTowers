using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInstantiation : MonoBehaviour
{
    public static FlowerInstantiation Instance;
    public Transform flowerFolder;
    public List<Transform> allFlowers;
    public List<Transform> pollinatedFlowers;
    public int maxPollinatedFlowers;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < flowerFolder.childCount; i++)
        {
            if (flowerFolder.GetChild(i).CompareTag("Flower"))
            {
                allFlowers.Add(flowerFolder.GetChild(i));  
            }
        }
        StartCoroutine(InstantiateFlowerParticles());
    }

    private IEnumerator InstantiateFlowerParticles()
    {
        while (true)
        {
            for (int i = 0; i < (maxPollinatedFlowers - pollinatedFlowers.Count); i++)
            {
                Transform randomFlower = allFlowers[Random.Range(0, allFlowers.Count)];
                pollinatedFlowers.Add(randomFlower);
                randomFlower.GetComponent<Flower>().EnablePollinating();            
            }
            yield return new WaitUntil(() => pollinatedFlowers.Count <= 2);
            
        }
    }

    public void RemoveFlowerFromArray(Transform flower)
    {
        pollinatedFlowers.Remove(flower);
    }
}
