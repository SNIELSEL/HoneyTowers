using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    public List<Transform> grounds = new List<Transform>();
    public Transform gridTilesParent;
    public GameObject grid;

    public LayerMask path;
    private GameObject tileClone;

    public Bounds bounds;
    private void Start()
    {

        foreach (Transform ground in grounds)
        {
            bounds = ground.GetComponent<Collider>().bounds;
            for (int x = 0; x < ground.localScale.x; x++)
            {
                for (int z = 0; z < ground.localScale.z; z++)
                {
                    tileClone = Instantiate(grid, new Vector3(bounds.min.x + x, bounds.max.y, bounds.min.z + z), Quaternion.identity, gridTilesParent);

                    Collider[] hitColliders = Physics.OverlapBox(tileClone.transform.position, tileClone.transform.lossyScale, Quaternion.identity, path);

                    if (hitColliders.Length > 0)
                    {
                        Destroy(tileClone);
                    }
                }
            }
        }
        
    }
}
