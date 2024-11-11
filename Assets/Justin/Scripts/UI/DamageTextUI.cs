using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    public float speed = 2.0f; 
    public float amplitude = 0.5f;  
    public Vector3 startPosition;  
    public TextMeshProUGUI text;

    public float upSpeed;

    void Start()
    {
        startPosition = transform.localPosition;
        Destroy(transform.parent.gameObject, 2f);
    }

    void Update()
    {
        float newX = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(startPosition.x + newX, transform.localPosition.y, startPosition.z);

        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }
}
