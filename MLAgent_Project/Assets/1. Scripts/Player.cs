using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform racket;

    [SerializeField] private float speed = 1f;

    private void Update()
    {
        #region Ű���� ������ ����
        float z = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(0, 0, z) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -0.75f, 0.75f));
        #endregion

        #region ���콺 ������ ���� �����̱� & ����
        Vector2 mousePos = Input.mousePosition;

        mousePos.y = Mathf.Lerp(1.5f, 2.6f, mousePos.y / 1080);
       /* if (mousePos.x >= 540)      // ������
        {
            racket.gameObject.transform.localRotation = Quaternion.Euler(-90, -180, 90);
        }
        else
        {
            racket.gameObject.transform.localRotation = Quaternion.Euler(-90, -180, -65);
        }*/
        mousePos.x = Mathf.Lerp(27f, 29.25f, mousePos.x / 1920);

        racket.localPosition = new Vector3(racket.localPosition.x, mousePos.y, mousePos.x);
        #endregion
    }
}
