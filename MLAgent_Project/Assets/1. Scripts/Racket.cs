using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private bool myRacket = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Ball>(out Ball ball))
        {
            collision.collider.GetComponent<Rigidbody>().AddForce(myRacket == true ? 1 : -1, 0.5f, 0);
            //GameManager.Instance.aiAgent.GoodAction(1);
            GameManager.Instance.testAI.GoodAction(1);
        }
    }
}
