using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public BallSpawner ballSpawner;
    public Ball ball;

    public AIAgent aiAgent;

    public TMP_Text startCountText;
    public GameObject canvas;
    private float time  = 4;
    private bool startCheck = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
            Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }

        if (!startCheck)
        {
            time -= Time.deltaTime;
            startCountText.text = ((int)time).ToString();
            if (time < 1)
            {
                startCheck = true;
                canvas.SetActive(false);
                ballSpawner.BallThrow();
            }
        }
    }

    public void End(bool isAIWin)
    {
        canvas.SetActive(true);
        if (isAIWin)
        {
            startCountText.text = "AI WIN";
        }
        else
        {
            startCountText.text = "PLAYER WIN";
        }
        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
