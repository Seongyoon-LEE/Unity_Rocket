using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� �Ŵ����� ����ϱ� ���� �߰� 
public class GameManager : MonoBehaviour
{

    public static GameManager Instance; // �̱��� ���� :: ��ü������ �ѹ��� �ǰ� �ٸ� Ŭ�������� ���� �����ϱ� ����
    //Asteroid ������ ����
    public GameObject asteroidPrefab;
    //Asteroid ���� �ֱ� �� �ð�
    private float timePreve; // �ð� ����
    [Header("bool GameOver")] //Attribute �Ӽ� : ����Ƽ�� �о ����
    public bool isGameOver = false; //���ӿ��� ����
    [Header("CameraShake Logic")]
    public bool isShake = false; // ī�޶� ��鸲 ����
    public Vector3 PosCamera; // ī�޶� ��ġ ���� 
    public float beginTime = 0.5f; // ��鸲 �ð�
    [Header("HpBar UI")]
    public int hp;
    public int maxHp = 100;
    public Image hpBar; // ü�¹� Ui
    public Text hpText; // 
    [Header("GameOver obj")]
    public GameObject gameOverObj; // ���� ���� ������Ʈ 
    public Text scoreTxt;
    private float curScore = 0f; // ���� ����
    private float pluseScore = 1f; // ���� ������
    

    //public SceneManager scene;
    void Start()
    {
        //if(Instance == null)
        Instance = this; // �̱��� ���� 
        //else if(Instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        
            DontDestroyOnLoad(this.gameObject); // ���� ������ ���� ���� �Ŵ����� ������� �ʴ´�.
        //���� ������ ����ø� ���� 
        timePreve = Time.time; // ���� �ð� ���� 
        hp = maxHp; // ü�� �ʱ�ȭ
        //gameOverObj = GetComponent<GameObject>();
    }

    
    void Update()
    {
        if (isGameOver == true) // ���� ������ �Ǹ� 
        {
            return; // ���⼭ ���� �������� ���� ���� ����
        }

        //����� - �����ð� = �귯�� �ð� 
        if (Time.time - timePreve > 2.5f && !isGameOver)
        {

            AsteroidSpawn();
        }

        if (isShake == true) // ���࿡ ī�޶� ��鸮��
        {
            float x = Random.Range(-0.1f, 0.1f); // �������� x��ǥ ����
            float y = Random.Range(-0.1f, 0.1f); // �������� y��ǥ ����
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - beginTime > 0.3f) // 0.3�ʰ� ������ ��鸲 ����
            {
                isShake = false; // ī�޶� ��鸲 ����
                Camera.main.transform.position = PosCamera; // ī�޶� ���� ��ġ�� ����
            }
        }
        ScoreCount();
        //Math.FloorToInt() float ������ �۰ų� ���� ū ������ ��ȯ �Ѵ�.
        //Math.FloorToInt(3.7)�� ��� ���� 3�� ��ȯ
        //Math.FloorToInt(3.2)�� ��� ���� -4�� ��ȯ 
        //Math.FloorToInt(-3.2)�� ��� ���� -4�� ��ȯ
        //Math.FloorToInt(-3.7)�� ��� ���� -4�� ��ȯ

    }

    private void ScoreCount()
    {
        curScore += pluseScore * Time.realtimeSinceStartup; // ���� ����
        //Time.realtimeSinceStartup : ������ ������ ������ �ð��� �ʴ����� ��ȯ readonly �Ӽ�
        scoreTxt.text = $" {Mathf.FloorToInt(curScore)}"; // ���� UI ���� 
    }

    public void TurnOn()
    {
        isShake = true; // ī�޶� ��鸲 ����
        PosCamera = Camera.main.transform.position; // ��鸮�� ���� ī�޶� ��ġ�� ����
        beginTime = Time.time; // ī�޶� ��鸮�� ������ �ð� ���� 
        
    }
    private void AsteroidSpawn()
    {
        float randomY = Random.Range(-4.0f, 4.0f); //�������� y��ǥ ����
        GameObject _asteroid = PoolingManager.p_Instance.GetAsteroid();
        if (_asteroid != null)
        {
            _asteroid.transform.position = new Vector3(12f, randomY, 0f); // ��ġ ����
            _asteroid.SetActive(true);
        }
        timePreve = Time.time; //����ð� ����
    }
    public void RoketHealtPoint(int damage)
    {
        hp -= damage; // ü�� ����
        hp = Mathf.Clamp(hp, 0, 100); //ü�� 0~100���� ����
        hpBar.fillAmount = (float)hp / (float)maxHp; // ü�¹� UI ����
        hpText.text = $"HP :<color=#ff9900>{hp}</color>"; // ü�� UI ����
        if(hp <= 0)
        {
            isGameOver = true; // ���� ����
            gameOverObj.SetActive(true) ; // ���ӿ��� ������Ʈ Ȱ��ȭ
            //���ӿ�����Ʈ�� Ȱ��ȭ ��Ȱ��ȭ �ϴ� �Լ�
            Invoke("LobbySceneMove", 3.0f);
            // ��Ʈ�� ���ڸ� �о ���ϴ� �ð��� ȣ���ϴ� �Լ� 
        }
    }
    public void LobbySceneMove()
    {

        SceneManager.LoadScene("LobbyScene"); // ���� �κ������ �ε�

    }

}
