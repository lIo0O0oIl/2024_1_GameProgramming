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
        if (z != 0f) z *= -1;
        transform.position += new Vector3(0, 0, z) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -0.75f, 0.75f));
        anim.MoveAnim(z);
        #endregion

        #region ���콺 ������ ���� �����̱� & ����
        Vector2 mousePos = Input.mousePosition;

        mousePos.y = Mathf.Lerp(1f, 1.9f, mousePos.y / 1080);
        if (mousePos.x >= 540)      // ������
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, 90);
        }
        else
        {
            handTarget.localRotation = Quaternion.Euler(-90, -180, -65);
        }
        mousePos.x = Mathf.Lerp(-0.29f, 0.65f, mousePos.x / 1920);

        // �����κ�
        if (Input.GetMouseButtonDown(0))        // AI �� �ٶ󺸴� ���� �������� �������� �б�
        {
            Debug.Log("�������� �б�");
        }
        if (Input.GetMouseButtonDown(1))       // ���������� �б�
        {
            Debug.Log("���������� �б�");
        }

        handTarget.localPosition = new Vector3(mousePos.x, mousePos.y, handTarget.localPosition.z);
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
