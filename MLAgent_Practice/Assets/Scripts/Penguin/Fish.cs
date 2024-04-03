using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    /*private float fishSpeed;
    private float nextActionTime = -1;
    private Vector3 targetPosition;

    private void FixedUpdate()
    {
        Swim();
    }

    private void Swim()
    {
        if (Time.fixedTime >= nextActionTime)
        {
            // 새로운 위치를 타겟팅하고 nextActionTime 을 다시 셋팅한다.
            fishSpeed = Random.Range(0.1f, 0.8f);
            targetPosition = PenguinArea.ChooseRendomPosition(transform.parent.position, 100f, 260f, 2f, 13f);
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

            float timeToTarget = Vector3.Distance(transform.position, targetPosition) / fishSpeed;
            nextActionTime = Time.fixedTime - timeToTarget;
        }
        else
        {
            // 타겟팅된 위치를 향해서 이동한다.
            transform.position += transform.forward * Time.fixedDeltaTime * fishSpeed;
        }
    }*/

    private float fishSpeed;
    private float nextActionTime = -1f;
    private Vector3 targetPosition;

    private void FixedUpdate()
    {
        Swim();
    }

    private void Swim()
    {
        if (Time.fixedTime >= nextActionTime)
        {
            // 새로운 위치를 타겟팅 하고 nextActoinTime을 다시 세팅
            fishSpeed = Random.Range(0.1f, 0.8f);
            targetPosition = PenguinArea.ChooseRandomPosition(transform.parent.position, 0f, 360f, 0f, 9f);
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

            float timeToTarget = Vector3.Distance(transform.position, targetPosition) / fishSpeed;
            nextActionTime = Time.fixedTime + timeToTarget;

        }
        else
        {
            // 타겟팅 된 위치를 향해 이동한다.
            transform.position += transform.forward * Time.fixedDeltaTime * fishSpeed;
        }
    }
}
