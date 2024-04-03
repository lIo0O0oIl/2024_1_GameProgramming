using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PenguinAgent : Agent
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public GameObject heartPrefab;
    public GameObject regurgitatedFishPrefab;

    private PenguinArea penguinArea;
    private new Rigidbody rigidbody;
    private GameObject babyPenguin;
    private bool IsFull;

    public override void Initialize()
    {
        penguinArea = transform.parent.Find("PenguinArea").GetComponent<PenguinArea>();
        babyPenguin = penguinArea.penguinBady;
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        IsFull = false;
        penguinArea.ResetArea();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 관측되는 값의 개수는 8개
        sensor.AddObservation(IsFull);
        sensor.AddObservation(Vector3.Distance(transform.position, babyPenguin.transform.position));
        sensor.AddObservation((babyPenguin.transform.position - transform.position).normalized);
        sensor.AddObservation(transform.forward);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var DiscreteActions = actions.DiscreteActions;

        // 앞으로 가지 않을지(0), 갈지(1)
        float forwardAmount = DiscreteActions[0];

        // 회전을 안할지(0), 왼쪽으로 회전할지(1), 오른쪽으로 회전할지(2)
        float turnAmount = 0f;
        if (DiscreteActions[1] == 1)
        {
            turnAmount = -1f;
        }
        else if (DiscreteActions[1] == 2)
        {
            turnAmount = 1f;
        }

/*        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedTime);
        transform.Rotate(Vector3.up * turnAmount * turnSpeed * Time.fixedTime);*/

        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var DiscrteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.W))
        {
            DiscrteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            DiscrteActionsOut[1] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            DiscrteActionsOut[1] = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("FISH"))
        {
            EatFish(collision.gameObject);
        }
        else if (collision.transform.CompareTag("BABY_PENGUIN"))
        {
            RegurgitateFish();
        }
    }

    private void EatFish(GameObject fishObject)
    {
        if (IsFull) return;
        IsFull = true;

        penguinArea.RemoveFishInLIst(fishObject);
        AddReward(1f);
    }

    private void RegurgitateFish()
    {
        if (!IsFull) return;
        IsFull = false;

        GameObject regurgitatedFish = Instantiate(regurgitatedFishPrefab);
        regurgitatedFish.transform.parent = transform.parent;
        regurgitatedFish.transform.localPosition = babyPenguin.transform.localPosition + Vector3.up * 0.01f;
        Destroy(regurgitatedFish, 4f);

        GameObject heart = Instantiate(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.localPosition = babyPenguin.transform.localPosition + Vector3.up;
        Destroy(heart, 4f);

        AddReward (1f);

        if (penguinArea.remainingFish <= 0)
        {
            EndEpisode();
        }
    }
}
