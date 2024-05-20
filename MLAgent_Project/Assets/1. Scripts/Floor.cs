using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private BallSpawner ballSpawner;

    public void BallEnter(GameObject ball)
    {
        Destroy(ball);
        ball.SetActive(false);
        Debug.Log("∞‘¿” ≥°");
        ballSpawner.BallSpawn();
    }
}
