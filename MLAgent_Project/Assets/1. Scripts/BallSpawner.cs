using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    private void Start()
    {
        //StartCoroutine(BallRoutine());
        //BallSpawn();
    }

/*    private IEnumerator BallRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            BallSpawn();
        }
    }*/

    public void BallSpawn()
    {
        GameObject throwBall = Instantiate(ball, transform.position, Quaternion.identity, transform);
        Rigidbody ballRigidbody = throwBall.GetComponentInChildren<Rigidbody>();

        float angle = Random.Range(-15f, 15f);      // ȸ����
        float zForce, yForce;  // Ź���뿡�� ������ ���� �� -40���� ���� ��. ����.    // ���� ���� ��

        zForce = Mathf.Lerp(-55f, -45f, angle / 15f);
        yForce = Mathf.Lerp(35f, 25f, angle / 15f);

        ballRigidbody.AddForce(new Vector3(zForce, yForce, angle));
        Debug.Log($"������ {zForce}��ŭ, ���� {yForce}��ŭ, ������ {angle}��.");
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