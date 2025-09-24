using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float h = 0f; //A,d 키를 받기 위한 변수  
    private float v = 0f; //W,S키를 받기 위한 변수
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source; //오디오소스
    public AudioClip hitClip; //오디오클립 
    private Vector2 StartPos = Vector2.zero; //시작위치
    Rigidbody2D rb;

    public Transform firePos; //발사위치
    public GameObject coinPrefab; //코인 프리팹
    public delegate void ShakeHandler();
    public static event ShakeHandler OnShake;

    void Start()
    {
        source = GetComponent<AudioSource>(); //오디오소스 컴포넌트 가져오기
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //GameManager.Instance.isGameOver = false;
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver == true)
            return;
        if (Application.platform == RuntimePlatform.WindowsEditor) //Windows 플랫폼일때
        {
            Vector3 speed = rb.velocity;
            speed.x = 4 * h;
            speed.y = 4 * v;
            rb.velocity = speed;
            //h = Input.GetAxis("Horizontal"); //A,d키를 받기 위한 변수
            //v = Input.GetAxis("Vertical"); //W,S키를 받기 위한 변수
            //Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
            //tr.Translate(Normal.normalized * speed * Time.deltaTime); //A,d키를 받기 위한 변수

        }
        if (Application.platform == RuntimePlatform.Android) //안드로이드 플랫폼일때
        {
            if (Input.touchCount > 0) //한번이라도 터치가 되었다면....
            {
                Mobile();

            }

        }
        if (Application.platform == RuntimePlatform.IPhonePlayer) //아이폰 플랫폼일때
        {
            if (Input.touchCount > 0) //한번이라도 터치가 되었다면....
            {


                Mobile();


            }
            #region 초보자 로직
            // if (tr.position.x >=7.6f)
            //     tr.position = new Vector3(7.6f, tr.position.y, tr.position.z);
            //else if(tr.position.x <= -7.8f)
            //     tr.position = new Vector3(-7.8f, tr.position.y, tr.position.z);
            //if (tr.position.y >= 4.5f)
            //     tr.position = new Vector3(tr.position.x, 4.5f, tr.position.z);
            // else if (tr.position.y <= -4.5f)
            //     tr.position = new Vector3(tr.position.x, -4.5f, tr.position.z);
            #endregion
            #region 중급자 로직
            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), tr.position.z);
            //수학클래스(Mathf.Clamp(what?,min,max) //값을 제한하는 함수
            #endregion

        }
    }
    private void Mobile()
    {
        Touch touch = Input.GetTouch(0); //터치한 정보를 가져옴
                                         //GetTouch(0) //터치한 위치값을 배열에 저장 되었는 데 첫번째 터치한 위치를 가져옴
        switch (touch.phase) //터치한 상태를 유형을 가져옴
        {
            case TouchPhase.Began: //터치가 시작되었을때
                StartPos = touch.position; //터치한 위치를 저장
                break;
            case TouchPhase.Moved: //터치가 이동했을때
                Vector3 touchDelta = touch.position - StartPos; //터치한 위치 - 시작위치 거리를 구함
                Vector3 moveDir = new Vector3(touchDelta.x, touchDelta.y, 0f); //터치한 위치 - 시작위치 거리
                tr.Translate(moveDir.normalized * speed * Time.deltaTime); //터치한 위치 - 시작위치 거리
                StartPos = touch.position; //터치한 위치를 저장
                break;
        }
    }

    // trigger 충돌 처리 콜백 함수 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            // Destroy(col.gameObject); //충돌한 오브젝트 삭제(asteroid)
            Debug.Log($"충돌 :");
            var eff = Instantiate(effect, col.transform.position, Quaternion.identity);
            Destroy(eff, 1f); //이펙트 삭제
            GameManager.Instance.TurnOn(); //카메라 흔들림
            GameManager.Instance.RoketHealtPoint(50); //체력 감소
            source.PlayOneShot(hitClip, 1f); //소리 재생
                                             // 소리를 한번만 울려라 (무엇을?, 볼륨);

            OnShake();
        }

    }
    public void Fire()
    {
        GameObject coin = PoolingManager.p_Instance.GetCoin();
        if (coin != null)
        {
            coin.transform.position = firePos.position; // 총알 위치 설정
            coin.transform.rotation = firePos.rotation; // 총알 회전 설정
            coin.SetActive(true);
        }

        //소멸함수(What?, time?)
    }

    public void OnStickPos(Vector3 stickPos)
    {
        h = stickPos.x; v = stickPos.y;
    }
}
