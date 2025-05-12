using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asterroid : MonoBehaviour
{
    public float speed;
    private Transform tr;
    private readonly string coin = "COIN"; // 코인이 동적할당 될때 상수가 된다.

    void Start()
    {
        speed = Random.Range(10f, 20f); // 랜덤으로 속도 설정 
        tr = GetComponent<Transform>();     
    }

    
    void Update()
    {              //방향 * 속도 = velocity
        

        tr.Translate(Vector3.left * speed * Time.deltaTime);    // 왼쪽으로 이동 
        if(tr.position.x <= -10f)
        {
            Destroy(this.gameObject);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == coin)
        {
            Destroy(col.gameObject); // 충돌한 오브젝트(coin) 삭제
            Destroy(this.gameObject,0.1f); // 충돌한 운석 삭제
            Debug.Log("파괴");
        }
    }
}
