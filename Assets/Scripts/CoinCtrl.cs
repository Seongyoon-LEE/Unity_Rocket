using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    public float speed = 100f; //���� �ӵ�
    private Rigidbody2D rb2D;

    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        Invoke("CoinDisable", 2.2f); // 3�� �Ŀ� �Ѿ� ��Ȱ��ȭ
    }
    void CoinDisable()
    {
        this.gameObject.SetActive(false); // �Ѿ� ��Ȱ��ȭ
    }
}
