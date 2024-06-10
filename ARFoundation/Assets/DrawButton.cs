using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color drawLineColor;
    public DrawLineController drawLineController;
    private bool isPressed;

    public void OnPointerDown(PointerEventData eventData)       // 버튼이 눌렸을 때
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)     // 버튼이 때어졌을 때
    {
        isPressed = false;
        drawLineController.StopDrawLine();
    }

    private void Update()
    {
        if (isPressed)
        {
            drawLineController.StartDrawLine(drawLineColor);
        }
    }
}
