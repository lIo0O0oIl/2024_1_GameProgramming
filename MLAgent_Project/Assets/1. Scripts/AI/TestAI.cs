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
        transform.position += new Vector3(0, 0, z) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -0.75f, 0.75f));
        anim.MoveAnim(z);
        #endregion

        #region 마우스 움직임 따라 움직이기
        //Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));
        Vector3 x = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        x.y = 0;
        x.z = 0;
        Debug.Log(x);

        //Debug.Log(x);
        handTarget.localPosition += x;
        // 0.1 이 되면 손 반대로 움직여야함.
        #endregion

        #region 스윙하기
        if (Input.GetMouseButtonDown(0))        // AI 가 바라보는 곳을 기준으로 왼쪽으로 밀기
        {

        }

        if (Input.GetMouseButtonDown(2))       // 오른쪽으로 밀기
        {

        }
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
