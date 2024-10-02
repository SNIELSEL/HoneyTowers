using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float speed;
    public int attackPower;

    protected Transform objToChase;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    public virtual void ObjectToGet(Transform obj)
    {
        objToChase = obj;
    }
}
