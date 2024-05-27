using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask map;
    
    private float touchTime = 0;
    private int aiTableTouchCount = 0;
    private int playerTableTouchCount = 0;
    [SerializeField] private bool visiteAIArea = false;

    public bool die = false;

    private float delay = 0;
    private bool delayStart = false;

    public void BallInit()
    {
        touchTime = 0;
        aiTableTouchCount = 0;
        playerTableTouchCount = 0;
        visiteAIArea=false;
        delay = 0;
    }

    private void Update()
    {
        if (die) return;

        if (!delayStart)
        {
            if (Physics.SphereCast(transform.position, 0.05f, Vector3.down, out RaycastHit hit, 0.1f, map))
            {
                Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("¶¥");
                    GameManager.Instance.aiAgent.GameOver(!visiteAIArea);
                }

                if (hit.collider.CompareTag("AIArea"))
                {
                    Debug.Log("ai");
                    aiTableTouchCount++;
                    if (aiTableTouchCount == 2)
                    {
                        aiTableTouchCount = 0;
                        GameManager.Instance.aiAgent.GameOver(false);
                    }
                    playerTableTouchCount = 0;
                    visiteAIArea = true;
                }

                if (hit.collider.CompareTag("PlayerArea"))
                {
                    Debug.Log("player");
                    playerTableTouchCount++;
                    if (playerTableTouchCount == 2)
                    {
                        GameManager.Instance.aiAgent.GameOver(true);
                    }
                    aiTableTouchCount = 0;
                    visiteAIArea = false;
                }

                delayStart = true;
            }
            else
            {
                touchTime = 0;
            }
        }

        if (delayStart)
        {
            delay += Time.deltaTime;
            if (delay > 0.5f)
            {
                delay = 0;
                delayStart = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (die) return;
        touchTime += Time.deltaTime;
        if (touchTime > 2)
        {
            if (visiteAIArea)
            {
                GameManager.Instance.aiAgent.GameOver(true);
            }
             else   GameManager.Instance.aiAgent.GameOver(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (Selection.activeObject == gameObject)
        {
            bool collision = Physics.SphereCast(transform.position, 0.05f, Vector3.down, out RaycastHit hit, 0.1f);
            if (collision)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, 0.05f);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, 0.05f);

            }
            Gizmos.color = Color.white;
        }
    }
}
