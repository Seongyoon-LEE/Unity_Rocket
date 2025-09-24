using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class Aseteroid : MonoBehaviour
{
    public float Speed;
    private Transform tr;
    private readonly string coinTag = "COIN"; //코인 태그
    private readonly string playerTag = "Player";
    public TrailRenderer trailRenderer;

    void Start()
    {
        Speed = Random.Range(10f, 25f); //랜덤으로 속도 설정
        tr = GetComponent<Transform>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    void Update()
    {                  //방향 * 속도 = Velocity
        tr.Translate(Vector3.left * Speed * Time.deltaTime); //왼쪽으로 이동
    }
    private void OnEnable()
    {
        Invoke("AsteroidDisable", 1.2f); // 1초 후에 ateroid 비활성화
    }
    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    void AsteroidDisable()
    {
        this.gameObject.SetActive(false); // ateroid 비활성화
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == coinTag) //플레이어와 충돌시
        {
            col.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == playerTag)
            this.gameObject.SetActive(false);

    }

}
