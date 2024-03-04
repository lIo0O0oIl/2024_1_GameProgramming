using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FloorAgent : Agent
{
    public Transform ballTransform;
    private Rigidbody ballRigidbody;

    // 환경이 처음 시작될 떄 한번만 실행되는 초기화 함수 (Start())
    public override void Initialize()
    {
        ballRigidbody = ballTransform.GetComponent<Rigidbody>();
    }

    // 각 에피소드가 시작될 때 호출되는 함수 => 게임한판 마다 실행. 환경의 상태를 초기화하는 역할
    public override void OnEpisodeBegin()
    {
        // Floor : x, z 축 기준으로 무작위하게 살짝 회전시킴
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));

        // Ball : velocity 와 위치를 초기화 한다.
        ballRigidbody.velocity = new Vector3(0, 0, 0);
        ballTransform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, Random.Range(-1.5f, 1.5f));
    }

    // input 으로 어떤 값을 넣어줄지. 관측은 시야, 백터 관측이 있는데 얘는 백터값. 자기에게 공과 바닥 정보를 줌.
    // 에이전트의 관측을 설정하는 함수 => Vector observation(백터 관측)
    public override void CollectObservations(VectorSensor sensor)
    {
        // 에이전트의 센서가 관측하는 값의 개수는 8개 이다.
        sensor.AddObservation(transform.rotation.x);
        sensor.AddObservation(transform.rotation.z);
        sensor.AddObservation(ballRigidbody.velocity);
        sensor.AddObservation(ballTransform.position - transform.position);
    }

    // 에이전트의 행동을 설정하는 함수
    public override void OnActionReceived(ActionBuffers actions)
    {
        // continuous Action 2개
        var ContinuousActions = actions.ContinuousActions;
        float z_rotation = Mathf.Clamp(ContinuousActions[0], -1f, 1f);
        float x_rotation = Mathf.Clamp(ContinuousActions[1], -1f, 1f);

        transform.Rotate(new Vector3(0, 0, 1), z_rotation);
        transform.Rotate(new Vector3(1, 0, 0), x_rotation);         // 실제 행동


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }
}
