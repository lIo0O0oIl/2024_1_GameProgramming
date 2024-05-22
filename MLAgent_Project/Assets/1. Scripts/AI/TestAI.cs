using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    private AIAnimation anim;

    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform handTarget;

    private void Awake()
    {
        anim = GetComponent<AIAnimation>();
    }

    private void Update()
    {
        #region 키보드 움직임 관련
        float z = Input.GetAxisRaw("Horizontal");
        if (z != 0f) z *= -1;
        transform.position += new Vector3(0, 0, z) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -0.75f, 0.75f));
        anim.MoveAnim(z);
        #endregion

        #region 마우스 움직임 따라 움직이기 & 스윙
        Vector2 mousePos = Input.mousePosition;

        mousePos.y = Mathf.Lerp(1f, 1.9f, mousePos.y / 1080);
        if (mousePos.x >= 540)      // 오른쪽
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, 90);
        }
        else
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, -65);
        }
        mousePos.x = Mathf.Lerp(-0.29f, 0.65f, mousePos.x / 1920);

        // 스윙부분
        if (Input.GetMouseButtonDown(0))        // AI 가 바라보는 곳을 기준으로 왼쪽으로 밀기
        {
            Debug.Log("왼쪽으로 밀기");
        }
        if (Input.GetMouseButtonDown(1))       // 오른쪽으로 밀기
        {
            Debug.Log("오른쪽으로 밀기");
        }

        handTarget.localPosition = new Vector3(mousePos.x, mousePos.y, handTarget.localPosition.z);
        #endregion
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
