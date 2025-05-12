using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BGNearMove : MonoBehaviour
{
    public float speed; //배경속도
    public Transform tr;
    public BoxCollider2D box2D;
    private float width;//폭
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        tr = GetComponent<Transform>();
        box2D = GetComponent<BoxCollider2D>();
        width = box2D.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver == true)
        {
            return; // 여기서 하위 로직으로 진행되지 않음 
        }
        if(tr.position.x <= -width*1.8)
        {
            Vector2 ofset = new Vector2(width * 2.5f, 0f);
            tr.position = (Vector2)tr.position + ofset;
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
