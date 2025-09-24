using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 매니저를 사용하기 위해 추가 
public class GameManager : MonoBehaviour
{

    public static GameManager Instance; // 싱글톤 패턴 :: 객체생성은 한번만 되고 다른 클래스에서 쉽게 접근하기 위해
    //Asteroid 프리팹 생성
    public GameObject asteroidPrefab;
    //Asteroid 생성 주기 및 시간
    private float timePreve; // 시간 저장
    [Header("bool GameOver")] //Attribute 속성 : 유니티가 읽어서 실행
    public bool isGameOver = false; //게임오버 여부
    [Header("CameraShake Logic")]
    public bool isShake = false; // 카메라 흔들림 여부
    public Vector3 PosCamera; // 카메라 위치 저장 
    public float beginTime = 0.5f; // 흔들림 시간
    [Header("HpBar UI")]
    public int hp;
    public int maxHp = 100;
    public Image hpBar; // 체력바 Ui
    public Text hpText; // 
    [Header("GameOver obj")]
    public GameObject gameOverObj; // 게임 오버 오브젝트 
    public Text scoreTxt;
    private float curScore = 0f; // 현재 점수
    private float pluseScore = 1f; // 점수 증가량
    

    //public SceneManager scene;
    void Start()
    {
        //if(Instance == null)
        Instance = this; // 싱글톤 패턴 
        //else if(Instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        
            DontDestroyOnLoad(this.gameObject); // 다음 씬으로 가도 게임 매니저가 사라지지 않는다.
        //게임 시작전 현재시를 저장 
        timePreve = Time.time; // 현재 시간 저장 
        hp = maxHp; // 체력 초기화
        //gameOverObj = GetComponent<GameObject>();
    }

    
    void Update()
    {
        if (isGameOver == true) // 게임 오버가 되면 
        {
            return; // 여기서 하위 로직으로 진행 되지 않음
        }

        //현재시 - 지난시간 = 흘러간 시간 
        if (Time.time - timePreve > 2.5f && !isGameOver)
        {

            AsteroidSpawn();
        }

        if (isShake == true) // 만약에 카메라가 흔들리면
        {
            float x = Random.Range(-0.1f, 0.1f); // 랜덤으로 x좌표 설정
            float y = Random.Range(-0.1f, 0.1f); // 랜덤으로 y좌표 설정
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - beginTime > 0.3f) // 0.3초가 지나면 흔들림 종료
            {
                isShake = false; // 카메라 흔들림 종료
                Camera.main.transform.position = PosCamera; // 카메라 원래 위치로 복귀
            }
        }
        ScoreCount();
        //Math.FloorToInt() float 값보다 작거나 같은 큰 정수를 반환 한다.
        //Math.FloorToInt(3.7)인 경우 정수 3을 반환
        //Math.FloorToInt(3.2)인 경우 정수 -4를 반환 
        //Math.FloorToInt(-3.2)인 경우 정수 -4를 반환
        //Math.FloorToInt(-3.7)인 경우 정수 -4를 반환

    }

    private void ScoreCount()
    {
        curScore += pluseScore * Time.realtimeSinceStartup; // 점수 증가
        //Time.realtimeSinceStartup : 게임이 시작한 이후의 시간을 초단위로 반환 readonly 속성
        scoreTxt.text = $" {Mathf.FloorToInt(curScore)}"; // 점수 UI 갱신 
    }

    public void TurnOn()
    {
        isShake = true; // 카메라 흔들림 시작
        PosCamera = Camera.main.transform.position; // 흔들리기 전에 카메라 위치값 저장
        beginTime = Time.time; // 카메라가 흔들리기 시작한 시간 저장 
        
    }
    private void AsteroidSpawn()
    {
        float randomY = Random.Range(-4.0f, 4.0f); //랜덤으로 y좌표 설정
        GameObject _asteroid = PoolingManager.p_Instance.GetAsteroid();
        if (_asteroid != null)
        {
            _asteroid.transform.position = new Vector3(12f, randomY, 0f); // 위치 지정
            _asteroid.SetActive(true);
        }
        timePreve = Time.time; //현재시간 저장
    }
    public void RoketHealtPoint(int damage)
    {
        hp -= damage; // 체력 감소
        hp = Mathf.Clamp(hp, 0, 100); //체력 0~100으로 제한
        hpBar.fillAmount = (float)hp / (float)maxHp; // 체력바 UI 갱신
        hpText.text = $"HP :<color=#ff9900>{hp}</color>"; // 체력 UI 갱신
        if(hp <= 0)
        {
            isGameOver = true; // 게임 오버
            gameOverObj.SetActive(true) ; // 게임오버 오브젝트 활성화
            //게임오브젝트를 활성화 비활성화 하는 함수
            Invoke("LobbySceneMove", 3.0f);
            // 스트링 문자를 읽어서 원하는 시간에 호출하는 함수 
        }
    }
    public void LobbySceneMove()
    {

        SceneManager.LoadScene("LobbyScene"); // 씬을 로비씬으로 로딩

    }

}
