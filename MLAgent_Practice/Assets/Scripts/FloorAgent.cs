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

    // ȯ���� ó�� ���۵� �� �ѹ��� ����Ǵ� �ʱ�ȭ �Լ� (Start())
    public override void Initialize()
    {
        ballRigidbody = ballTransform.GetComponent<Rigidbody>();
    }

    // �� ���Ǽҵ尡 ���۵� �� ȣ��Ǵ� �Լ� => �������� ���� ����. ȯ���� ���¸� �ʱ�ȭ�ϴ� ����
    public override void OnEpisodeBegin()
    {
        // Floor : x, z �� �������� �������ϰ� ��¦ ȸ����Ŵ
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));

        // Ball : velocity �� ��ġ�� �ʱ�ȭ �Ѵ�.
        ballRigidbody.velocity = new Vector3(0, 0, 0);
        ballTransform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, Random.Range(-1.5f, 1.5f));
    }

    // input ���� � ���� �־�����. ������ �þ�, ���� ������ �ִµ� ��� ���Ͱ�. �ڱ⿡�� ���� �ٴ� ������ ��.
    // ������Ʈ�� ������ �����ϴ� �Լ� => Vector observation(���� ����)
    public override void CollectObservations(VectorSensor sensor)
    {
        // ������Ʈ�� ������ �����ϴ� ���� ������ 8�� �̴�.
        sensor.AddObservation(transform.rotation.x);
        sensor.AddObservation(transform.rotation.z);
        sensor.AddObservation(ballRigidbody.velocity);
        sensor.AddObservation(ballTransform.position - transform.position);
    }

    // ������Ʈ�� �ൿ�� �����ϴ� �Լ�
    public override void OnActionReceived(ActionBuffers actions)
    {
        // continuous Action 2��
        var ContinuousActions = actions.ContinuousActions;
        float z_rotation = Mathf.Clamp(ContinuousActions[0], -1f, 1f);
        float x_rotation = Mathf.Clamp(ContinuousActions[1], -1f, 1f);

        transform.Rotate(new Vector3(0, 0, 1), z_rotation);
        transform.Rotate(new Vector3(1, 0, 0), x_rotation);         // ���� �ൿ


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }
}
