using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIAgent : Agent
{
    public GameObject nowBall;      // 지금 공 

    // 처음 딱 한번. 초기화 함수
    public override void Initialize()
    {

    }

    // 에피소드가 실행될 때마다
    public override void OnEpisodeBegin()
    {

    }

    // 관측값
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    // AI 행동
    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousAction = actions.ContinuousActions;
    }

    // 내가 조종할 수 있음.
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionsOut = actionsOut.ContinuousActions;
        ContinuousActionsOut[0] = -Input.GetAxis("Horizontal");
        ContinuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    public void GoodAction(float reward)
    {
        Debug.Log($"상을 받아요! {reward} 만큼.");
        //AddReward(reward);
    }

    public void GameOver()
    {
        Debug.Log("게임끝");
        //EndEpisode();
    }
}

/*

손이 자동으로 움직여짐~

*/