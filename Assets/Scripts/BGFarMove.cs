using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFarMove : MonoBehaviour
{
    public float speed; // ��� �ӵ�
    public Transform tr;
    public BoxCollider2D box2D;
    private float width; // �� 
    void Start()
    {
      tr = GetComponent<Transform>();  
      box2D = GetComponent<BoxCollider2D>();
      speed = 10f;
      width = box2D.size.x; // �ڽ� �ݶ��̴��� ������ x���� �ʺ�� ���� 
    }

    
    void Update()
    {
        if(GameManager.Instance.isGameOver == true)

        {
            return;
        }
        
        if ((tr.position.x<=-width*1.8f)) // Ʈ�������� ������ x���� �ʺ񺸴� ������ 
        {
            Reposition();
        }
        tr.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Reposition()
    {
        Vector2 ofsset = new Vector2(width * 2.5f, 0f); // �������� �ʺ��� 2.5f�� ����
        tr.position = (Vector2)tr.position + ofsset; // Ʈ�������� �����ǿ� �������� ����
    }
}
