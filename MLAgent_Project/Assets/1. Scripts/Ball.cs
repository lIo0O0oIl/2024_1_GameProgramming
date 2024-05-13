using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;

    private void Update()
    {
        if (Physics.SphereCast(transform.position, 0.05f, Vector3.down, out RaycastHit hit, 0.1f, mapLayer))
        {
            if (hit.collider.TryGetComponent<Floor>(out Floor floor))
            {
                floor.BallEnter(gameObject);
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
