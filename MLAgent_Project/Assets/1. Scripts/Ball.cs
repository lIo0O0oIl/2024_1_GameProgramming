using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;

    private float delay = 0.25f;
    private float nowTime = 0;
    private bool is_delay = false;

    private int tableTouchCount = 0;

    private void Start()
    {
        GameManager.Instance.aiAnimation.SetHeadAim(this.gameObject);
    }

    private void Update()
    {
        if (is_delay == false)
        {
            if (Physics.SphereCast(transform.position, 0.05f, Vector3.down, out RaycastHit hit, 0.1f, mapLayer))
            {
                  if (hit.collider.CompareTag("Ground"))
                  {
                    //GameManager.Instance.aiAgent.GameOver();
                    GameManager.Instance.testAI.GameOver();
                    Destroy(this.gameObject);
                  }

                if (hit.collider.CompareTag("AIArea"))
                {
                    tableTouchCount++;
                    if (tableTouchCount == 2)
                    {
                        //GameManager.Instance.aiAgent.GameOver();
                        GameManager.Instance.testAI.GameOver();
                    }

                    //GameManager.Instance.aiAgent.GoodAction(-1);
                    GameManager.Instance.testAI.GoodAction(-1);
                    is_delay = true;

                }

                if (hit.collider.CompareTag("MyArea"))
                {
                    //GameManager.Instance.aiAgent.GoodAction(1);
                    GameManager.Instance.testAI.GoodAction(1);
                    is_delay = true;
                    tableTouchCount++;
                }
            }
        }
        else
        {
            nowTime += Time.deltaTime;
            if (nowTime >= delay)
            {
                is_delay = false;
                nowTime = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Selection.activeObject == gameObject)
        {
            bool collision = Physics.SphereCast(transform.position, 0.05f, Vector3.down, out RaycastHit hit, 0.1f, mapLayer);
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
