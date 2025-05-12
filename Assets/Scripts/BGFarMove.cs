using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFarMove : MonoBehaviour
{
    public float speed; // 배경 속도
    public Transform tr;
    public BoxCollider2D box2D;
    private float width; // 폭 
    void Start()
    {
      tr = GetComponent<Transform>();  
      box2D = GetComponent<BoxCollider2D>();
      speed = 10f;
      width = box2D.size.x; // 박스 콜라이더의 사이즈 x값을 너비로 지정 
    }

    
    void Update()
    {
        if(GameManager.Instance.isGameOver == true)

        {
            return;
        }
        
        if ((tr.position.x<=-width*1.8f)) // 트랜스폼의 포지션 x값이 너비보다 작으면 
        {
            Reposition();
        }
        tr.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Reposition()
    {
        Vector2 ofsset = new Vector2(width * 2.5f, 0f); // 오프셋을 너비의 2.5f로 지정
        tr.position = (Vector2)tr.position + ofsset; // 트랜스폼의 포지션에 오프셋을 더함
    }
}
