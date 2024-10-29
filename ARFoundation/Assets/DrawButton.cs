using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color drawLineColor;
    public DrawLineController drawLineController;
    private bool isPressed;

    public void OnPointerDown(PointerEventData eventData)       // ��ư�� ������ ��
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)     // ��ư�� �������� ��
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
