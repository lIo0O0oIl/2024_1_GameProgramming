using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private bool playerRacket = false;

    public float racketForce = 160;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
                Debug.Log("»˚¿ª¡‡ø‰!");
                collision.collider.GetComponent<Rigidbody>().AddForce(playerRacket == true ? -160 : racketForce, 70f, 0);
                GameManager.Instance.aiAgent.GoodAction(1);
            
        }
    }
}
