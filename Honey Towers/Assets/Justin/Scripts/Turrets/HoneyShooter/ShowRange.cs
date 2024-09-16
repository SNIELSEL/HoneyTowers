using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRange : MonoBehaviour
{
    public LineRenderer circleRenderer;

    public int steps;
    public float radius;

    private void Start()
    {
        circleRenderer = GetComponent<LineRenderer>();
        RenderCircle();
    }

    private void RenderCircle()
    {
        circleRenderer.positionCount = steps;

        for (int i = 0; i < steps; i++)
        {
            float circumferenceProgress = (float)i / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float z = zScaled * radius;
            Vector3 currentPosition = new Vector3(x, 0, z);

            circleRenderer.SetPosition(i, currentPosition);
        }

        circleRenderer.positionCount++;
        circleRenderer.SetPosition(circleRenderer.positionCount - 1, circleRenderer.GetPosition(0));
    }
}
