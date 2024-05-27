using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Transform ball;
    private Rigidbody ballRigidbody;

    public void BallThrow()
    {
        ball.gameObject.SetActive(true);
        if (ballRigidbody == null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
        }

        ballRigidbody.velocity = Vector3.zero;
        ball.rotation = Quaternion.identity;

        float angle = Random.Range(-12.5f, 12.5f);      // ȸ����
        float zForce, yForce;  // Ź���뿡�� ������ ���� �� -40���� ���� ��. ����.    // ���� ���� ��

        zForce = Mathf.Lerp(-75f, -65f, angle / 15f);
        yForce = Mathf.Lerp(35f, 45f, angle / 15f);

        ballRigidbody.AddForce(new Vector3(zForce, yForce, angle));
        //Debug.Log($"������ {zForce}��ŭ, ���� {yForce}��ŭ, ������ {angle}��.");
    }
}


/*

* ȸ���� 15�� ��
�Ѵ� ���� �� ������ -50, ���� 30���� ����
�Ѵ� ���� �� ������ -45, ���� 25���� ����

* ȸ���� 0�� ��
�Ѵ� ���� �� ������ -55, ���� 35���� ����
�Ѵ� ���� �� ������ -45, ���� 25���� ����

*/