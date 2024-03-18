using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MummyGoAgent : Agent
{
    /*   public Material goodMaterial;
       public Material badMaterial;
       private Material orginMaterial;
       private Renderer floorRenderer;

       public Transform targetTransform;
       private new Rigidbody rigidbody;

       // ó�� �ѹ� ����Ǵ� �ʱ�ȭ �Լ�
       public override void Initialize()
       {
           rigidbody = GetComponent<Rigidbody>();

           floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
           orginMaterial = floorRenderer.material;     // ������ ����
       }

       // �� ���Ǽҵ尡 ���۵� �� 
       public override void OnEpisodeBegin()
       {
           rigidbody.velocity = Vector3.zero;

           transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.05f, Random.Range(-4f, 4f));
           targetTransform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.55f, Random.Range(-4f, 4f));

           StartCoroutine(RecoverFloor());
       }

       private IEnumerator RecoverFloor()
       {
           yield return new WaitForSeconds(0.2f);
           floorRenderer.material = orginMaterial;
       }

       // ������Ʈ�� ������ �����ϴ� �Լ� => ���� ����
       public override void CollectObservations(VectorSensor sensor)
       {
           //(3, 3 1 1) ���� �� 8��
           sensor.AddObservation(targetTransform.localPosition);
           sensor.AddObservation(transform.localPosition);
           sensor.AddObservation(rigidbody.velocity.x);
           sensor.AddObservation(rigidbody.velocity.z);
       }

       public override void OnActionReceived(ActionBuffers actions)
       {
           var ContinuousActions = actions.ContinuousActions;
           Vector3 direction = (Vector3.forward * ContinuousActions[0]) + (Vector3.right * ContinuousActions[1]);
           Debug.Log(direction);
           rigidbody.AddForce(direction.normalized * 50f);

           SetReward(-0.01f);
       }

       // ����� Agent�� �������� �����ϴ� ����� �����ϴ� �Լ�
   *//*    public override void Heuristic(in ActionBuffers actionsOut)
       {
           // ����Ű �Է��� ���� �ൿ���� �����Ѵ�.
           var ContinuousActionsOut = actionsOut.ContinuousActions;
           ContinuousActionsOut[0] = Input.GetAxis("Vertical");
           ContinuousActionsOut[1] = Input.GetAxis("Horizontal");
       }*//*

       private void OnCollisionEnter(Collision collision)
       {
           if (collision.collider.CompareTag("WALL"))
           {
               floorRenderer.material = badMaterial;
               SetReward(-1f);
               EndEpisode();
           }
           if (collision.collider.CompareTag("TARGET"))
           {
               floorRenderer.material =goodMaterial;
               SetReward(1f);
               //EndEpisode();
           }
       }*/

    public Transform targetTransforms;
    public Material orginMat;

    private new Rigidbody rigidbody;
    private Renderer floorRenderer;


    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();

        floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
        orginMat = floorRenderer.material;
    }

    public override void OnEpisodeBegin()
    {
        rigidbody.velocity = Vector3.zero;

        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.05f, Random.Range(-4f, 4f));
        targetTransforms.localPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));

        floorRenderer.material = orginMat;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // ���� ���� 8��
        sensor.AddObservation(targetTransforms.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rigidbody.velocity.x);
        sensor.AddObservation(rigidbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var ContinuosActions = actions.ContinuousActions;
        Debug.Log(ContinuosActions[0]);
        Vector3 dir = (Vector3.forward * ContinuosActions[0]) + (Vector3.right * ContinuosActions[1]);
        //Debug.Log(dir);
        rigidbody.AddForce(dir.normalized * 50f);

        SetReward(-0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuosActionsOut = actionsOut.ContinuousActions;

        ContinuosActionsOut[0] = Input.GetAxis("Vertical");
        ContinuosActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            floorRenderer.material.color = Color.red;
            SetReward(-1f);
            EndEpisode();
        }
        if (collision.collider.CompareTag("TARGET"))
        {
            floorRenderer.material.color = Color.blue;
            SetReward(1f);
        }
    }
}
