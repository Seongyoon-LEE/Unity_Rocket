using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asterroid : MonoBehaviour
{
    public float speed;
    private Transform tr;
    private readonly string coin = "COIN"; // ������ �����Ҵ� �ɶ� ����� �ȴ�.

    void Start()
    {
        speed = Random.Range(10f, 20f); // �������� �ӵ� ���� 
        tr = GetComponent<Transform>();     
    }

    
    void Update()
    {              //���� * �ӵ� = velocity
        

        tr.Translate(Vector3.left * speed * Time.deltaTime);    // �������� �̵� 
        if(tr.position.x <= -10f)
        {
            Destroy(this.gameObject);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == coin)
        {
            Destroy(col.gameObject); // �浹�� ������Ʈ(coin) ����
            Destroy(this.gameObject,0.1f); // �浹�� � ����
            Debug.Log("�ı�");
        }
    }
}
