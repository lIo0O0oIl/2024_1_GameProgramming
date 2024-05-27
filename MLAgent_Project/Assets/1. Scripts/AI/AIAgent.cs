using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIAgent : Agent
{
    public Transform ballTransform;
    private Rigidbody ballRigidbody;

    public Transform handTarget;
    public Racket racket;

    // 처음 딱 한번. 초기화 함수
    public override void Initialize()
    {
        ballRigidbody = ballTransform.GetComponent<Rigidbody>();
    }

    // 에피소드가 실행될 때마다
    public override void OnEpisodeBegin()
    {
        //ballTransform.localPosition = GameManager.Instance.ballSpawner.transform.localPosition;       // 위치 초기화
        GameManager.Instance.ball.BallInit();
        //GameManager.Instance.ballSpawner.BallThrow();          // 던져주기
    }

    // 관측값
    public override void CollectObservations(VectorSensor sensor)
    {
        // 3, 1, 1, 1, 1, 1, 3, 3, 1 = 15
        sensor.AddObservation(ballRigidbody.velocity);

        sensor.AddObservation(ballTransform.localPosition.x);
        sensor.AddObservation(ballTransform.localPosition.y);

        sensor.AddObservation(transform.localPosition.x);

        sensor.AddObservation(handTarget.localPosition.x);
        sensor.AddObservation(handTarget.localPosition.y);

        sensor.AddObservation(ballTransform.localPosition - handTarget.localPosition);
        sensor.AddObservation(ballTransform.localPosition - transform.localPosition);

        sensor.AddObservation(racket.racketForce);      // 1
    }

    // AI 행동
    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousAction = actions.ContinuousActions;
        float bodyZ = Mathf.Clamp(continuousAction[0], -1f, 1f);
        float racketX = Mathf.Clamp(continuousAction[1], -1f, 1f);
        float racketY = Mathf.Clamp(continuousAction[2], -1f, 1f);
        float racketForce = Mathf.Clamp(continuousAction[3], -1f, 1f);
        racketX = Mathf.Lerp(0, 1920, racketX);
        racketY = Mathf.Lerp(0, 1020, racketY);
        racketForce = Mathf.Lerp(40, 55, racketForce);

        // 몸의 움직임
        transform.localPosition += new Vector3(bodyZ, 0, 0) * 3 *  Time.deltaTime;
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -0.75f, 0.75f), transform.localPosition.y, transform.localPosition.z);

        // 손(라켓)의 움직임
        racketY = Mathf.Lerp(1f, 1.9f, racketY/ 1080);
       /* if (racketX >= 540)      // 오른쪽
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, 90);
        }
        else
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, -65);
        }*/
        racketX = Mathf.Lerp(-0.29f, 0.65f, racketX / 1920);
        handTarget.localPosition = new Vector3(racketX, racketY, handTarget.localPosition.z);

        AddReward(-1f / MaxStep);

        // 라켓힘
        racket.racketForce = racketForce;

        if (ballTransform.localPosition.y < -2f)
        {
            Debug.LogError("바닥으로만 감지하기");
        }
    }

    // 내가 조종할 수 있음.
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionsOut = actionsOut.ContinuousActions;
        ContinuousActionsOut[0] = Input.GetAxis("Horizontal") * -1;

        Vector2 mousePos = Input.mousePosition;
        ContinuousActionsOut[1] = mousePos.x;
        ContinuousActionsOut[2] = mousePos.y;
    }

    public void BallReset()
    {
        ballTransform.position = GameManager.Instance.ballSpawner.transform.position;       // 위치 초기화
        GameManager.Instance.ball.BallInit();
        GameManager.Instance.ballSpawner.BallThrow();
    }

    public void GoodAction(float reward)
    {
        Debug.Log($"상을 받아요! {reward} 만큼.");
        AddReward(reward);
    }

    public void GameOver(bool isAIWin)
    {
        Debug.Log("게임끝");
        AddReward(-1);
        //EndEpisode();

        Debug.Log(isAIWin);
        GameManager.Instance.ball.die = true;
        GameManager.Instance.End(isAIWin);
    }
}

/*

손이 자동으로 움직여짐~

*/