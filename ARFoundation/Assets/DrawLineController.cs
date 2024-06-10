using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineController : MonoBehaviour
{
    public Transform drawPoint;
    public GameObject linePrefab;
    private LineRenderer currentLine;
    public Transform lines;
    private List<LineRenderer> lineList = new List<LineRenderer>();

    private bool isDrawingLine;

    private void Update()
    {
        if (isDrawingLine)
        {
            DrawLineContinue();
        }
    }

    public void StartDrawLine(Color lineColor)
    {
        if (!isDrawingLine)
        {
            isDrawingLine = true;
            MakeLine(lineColor); ;
        }
    }

    public void StopDrawLine()
    {
        isDrawingLine = false;
        currentLine = null;
    }

    private void MakeLine(Color lineColor)
    {
        GameObject lineObj = Instantiate(linePrefab);
        lineObj.transform.SetParent(lines);
        lineObj.transform.position = Vector3.zero;
        lineObj.transform.localScale = new Vector3(1, 1, 1);

        currentLine = lineObj.GetComponent<LineRenderer>();
        currentLine.startColor = lineColor;
        currentLine.endColor = lineColor;
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, drawPoint.position);

        lineList.Add(currentLine);
    }

    private void DrawLineContinue()
    {
        currentLine.positionCount++;
        currentLine.SetPosition(currentLine.positionCount - 1, drawPoint.position);
    }
}
