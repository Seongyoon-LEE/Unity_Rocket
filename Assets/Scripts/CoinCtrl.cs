using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CircleCollider2D circle;
    public float speed = 100f;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(Vector2.right * speed,ForceMode2D.Impulse);
    }


    void Update()
    {
       
    }
}
