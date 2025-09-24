using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class Aseteroid : MonoBehaviour
{
    public float Speed;
    private Transform tr;
    private readonly string coinTag = "COIN"; //���� �±�
    private readonly string playerTag = "Player";
    public TrailRenderer trailRenderer;

    void Start()
    {
        Speed = Random.Range(10f, 25f); //�������� �ӵ� ����
        tr = GetComponent<Transform>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    void Update()
    {                  //���� * �ӵ� = Velocity
        tr.Translate(Vector3.left * Speed * Time.deltaTime); //�������� �̵�
    }
    private void OnEnable()
    {
        Invoke("AsteroidDisable", 1.2f); // 1�� �Ŀ� ateroid ��Ȱ��ȭ
    }
    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    void AsteroidDisable()
    {
        this.gameObject.SetActive(false); // ateroid ��Ȱ��ȭ
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == coinTag) //�÷��̾�� �浹��
        {
            col.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == playerTag)
            this.gameObject.SetActive(false);

    }

}
