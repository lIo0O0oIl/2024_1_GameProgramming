using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ruler : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public LineRenderer line;

    public Transform valueCanvas;
    public TextMeshProUGUI valueText;

    public void SetPointA(Vector3 position)
    {
        pointA.transform.position = position;
        line.SetPosition(0, position);
    }

    public void SetPointB(Vector3 position)
    {
        pointB.transform.position = position;
        line.SetPosition(1, position);
    }

    private void Update()
    {
        Vector3 rulerVector = pointB.position - pointA.position;
        valueCanvas.position = pointA.position + rulerVector * 0.5f + Vector3.up * 0.02f;

        float rulerValue = rulerVector.magnitude * 100f;
        valueText.text = string.Format("{0} cm", rulerValue.ToString("N2"));

        valueCanvas.LookAt(Camera.main.transform);
    }
}
