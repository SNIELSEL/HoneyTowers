using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
