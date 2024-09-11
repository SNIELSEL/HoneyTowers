using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    public GameObject grid;
    public GameObject highLight;

    public Bounds bounds;
    private void Start()
    {
        
        bounds = GetComponent<Collider>().bounds;
        print(bounds.max.y);
        for (int x = 0; x < transform.localScale.x; x++)
        {
            for (int z = 0; z < transform.localScale.z; z++)
            {
                Instantiate(grid, new Vector3(bounds.min.x + x, bounds.max.y, bounds.min.z + z), Quaternion.identity);
            }
        }
    }
}
