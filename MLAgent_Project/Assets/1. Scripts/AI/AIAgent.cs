using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIAgent : Agent
{
    public GameObject nowBall;      // ���� �� 

    // ó�� �� �ѹ�. �ʱ�ȭ �Լ�
    public override void Initialize()
    {

    }

    // ���Ǽҵ尡 ����� ������
    public override void OnEpisodeBegin()
    {

    }

    // ������
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    // AI �ൿ
    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousAction = actions.ContinuousActions;
    }

    // ���� ������ �� ����.
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionsOut = actionsOut.ContinuousActions;
        ContinuousActionsOut[0] = -Input.GetAxis("Horizontal");
        ContinuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    public void GoodAction(float reward)
    {
        Debug.Log($"���� �޾ƿ�! {reward} ��ŭ.");
        //AddReward(reward);
    }

    public void GameOver()
    {
        Debug.Log("���ӳ�");
        //EndEpisode();
    }
}

/*

���� �ڵ����� ��������~

*/