using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using UnityEngine.SceneManagement;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.Analytics;

public class Player : Agent
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    private Spawner spawner;
    private TextMeshPro scoreText;
    private int score;

    public override void Initialize()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawner = transform.parent.Find("Spawner").GetComponent<Spawner>();
        scoreText = transform.parent.Find("ScoreText").GetComponent<TextMeshPro>();
    }

    public override void OnEpisodeBegin()
    {
        GameStart();
        Vector3 position = transform.localPosition;
        position.y = 0f;
        transform.localPosition = position;
        direction = Vector3.zero;
    }

    // 관측함수
    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(transform.forward);
    }

    // 액션
    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        // 뛸까 말까
        if (discreteActions[0] == 1)
        {
            Jump();
        }

        AddReward(0.01f);
    }

    // 사람이 수동으로
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var DiscrteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.localPosition.y <= 3.5f)
            {
                DiscrteActionsOut[0] = 1;
                //Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        // 플레이어에 중력 적용
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // 방향에 따라 스프라이트 기울이기
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void Jump()
    {
        direction = Vector3.up * strength;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) 
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) 
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void GameStart()
    {
        score = 0;
        scoreText.text = score.ToString();
        foreach (var pipe in spawner.pipeList)
        {
            Destroy(pipe);
        }
        spawner.pipeList = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) 
        {
            AddReward(-1);
            EndEpisode();
        } 
        else if (other.gameObject.CompareTag("Scoring")) 
        {
            AddReward(1f);
            IncreaseScore();
        }
    }

    private void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }


}
