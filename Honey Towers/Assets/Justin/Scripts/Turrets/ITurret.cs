using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float speed;
    public int attackPower;

    protected Transform objToChase;

    public virtual void ObjectToGet(Transform obj)
    {
        objToChase = obj;
    }
}
