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
        #region Ű���� ������ ����
        float z = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(0, 0, z) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -0.75f, 0.75f));
        anim.MoveAnim(z);
        #endregion

        #region ���콺 ������ ���� �����̱�
        //Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));
        Vector3 x = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        x.y = 0;
        x.z = 0;
        Debug.Log(x);

        //Debug.Log(x);
        handTarget.localPosition += x;
        // 0.1 �� �Ǹ� �� �ݴ�� ����������.
        #endregion

        #region �����ϱ�
        if (Input.GetMouseButtonDown(0))        // AI �� �ٶ󺸴� ���� �������� �������� �б�
        {

        }

        if (Input.GetMouseButtonDown(2))       // ���������� �б�
        {

        }
        #endregion
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
