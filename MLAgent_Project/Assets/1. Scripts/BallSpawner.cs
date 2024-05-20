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

        float angle = Random.Range(-15f, 15f);      // 회전값
        float zForce, yForce;  // 탁구대에서 앞으로 가는 힘 -40쪽이 적은 힘. 절댓값.    // 위로 가는 힘

        zForce = Mathf.Lerp(-55f, -45f, angle / 15f);
        yForce = Mathf.Lerp(35f, 25f, angle / 15f);

        ballRigidbody.AddForce(new Vector3(zForce, yForce, angle));
        Debug.Log($"앞으로 {zForce}만큼, 위로 {yForce}만큼, 각도는 {angle}임.");
    }
}


/*

* 회전값 15일 때
둘다 많을 때 앞으로 -50, 위로 30까지 가능
둘다 적을 때 앞으로 -45, 위로 25까지 가능

* 회전값 0일 때
둘다 많을 때 앞으로 -55, 위로 35까지 가능
둘다 적을 때 앞으로 -45, 위로 25까지 가능

*/