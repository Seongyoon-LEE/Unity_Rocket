using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float h = 0f; //A,d Ű�� �ޱ� ���� ����  
    private float v = 0f; //W,SŰ�� �ޱ� ���� ����
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source; //������ҽ�
    public AudioClip hitClip; //�����Ŭ�� 
    private Vector2 StartPos = Vector2.zero; //������ġ
    Rigidbody2D rb;

    public Transform firePos; //�߻���ġ
    public GameObject coinPrefab; //���� ������
    public delegate void ShakeHandler();
    public static event ShakeHandler OnShake;

    void Start()
    {
        source = GetComponent<AudioSource>(); //������ҽ� ������Ʈ ��������
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //GameManager.Instance.isGameOver = false;
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver == true)
            return;
        if (Application.platform == RuntimePlatform.WindowsEditor) //Windows �÷����϶�
        {
            Vector3 speed = rb.velocity;
            speed.x = 4 * h;
            speed.y = 4 * v;
            rb.velocity = speed;
            //h = Input.GetAxis("Horizontal"); //A,dŰ�� �ޱ� ���� ����
            //v = Input.GetAxis("Vertical"); //W,SŰ�� �ޱ� ���� ����
            //Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
            //tr.Translate(Normal.normalized * speed * Time.deltaTime); //A,dŰ�� �ޱ� ���� ����

        }
        if (Application.platform == RuntimePlatform.Android) //�ȵ���̵� �÷����϶�
        {
            if (Input.touchCount > 0) //�ѹ��̶� ��ġ�� �Ǿ��ٸ�....
            {
                Mobile();

            }

        }
        if (Application.platform == RuntimePlatform.IPhonePlayer) //������ �÷����϶�
        {
            if (Input.touchCount > 0) //�ѹ��̶� ��ġ�� �Ǿ��ٸ�....
            {


                Mobile();


            }
            #region �ʺ��� ����
            // if (tr.position.x >=7.6f)
            //     tr.position = new Vector3(7.6f, tr.position.y, tr.position.z);
            //else if(tr.position.x <= -7.8f)
            //     tr.position = new Vector3(-7.8f, tr.position.y, tr.position.z);
            //if (tr.position.y >= 4.5f)
            //     tr.position = new Vector3(tr.position.x, 4.5f, tr.position.z);
            // else if (tr.position.y <= -4.5f)
            //     tr.position = new Vector3(tr.position.x, -4.5f, tr.position.z);
            #endregion
            #region �߱��� ����
            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), tr.position.z);
            //����Ŭ����(Mathf.Clamp(what?,min,max) //���� �����ϴ� �Լ�
            #endregion

        }
    }
    private void Mobile()
    {
        Touch touch = Input.GetTouch(0); //��ġ�� ������ ������
                                         //GetTouch(0) //��ġ�� ��ġ���� �迭�� ���� �Ǿ��� �� ù��° ��ġ�� ��ġ�� ������
        switch (touch.phase) //��ġ�� ���¸� ������ ������
        {
            case TouchPhase.Began: //��ġ�� ���۵Ǿ�����
                StartPos = touch.position; //��ġ�� ��ġ�� ����
                break;
            case TouchPhase.Moved: //��ġ�� �̵�������
                Vector3 touchDelta = touch.position - StartPos; //��ġ�� ��ġ - ������ġ �Ÿ��� ����
                Vector3 moveDir = new Vector3(touchDelta.x, touchDelta.y, 0f); //��ġ�� ��ġ - ������ġ �Ÿ�
                tr.Translate(moveDir.normalized * speed * Time.deltaTime); //��ġ�� ��ġ - ������ġ �Ÿ�
                StartPos = touch.position; //��ġ�� ��ġ�� ����
                break;
        }
    }

    // trigger �浹 ó�� �ݹ� �Լ� 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            // Destroy(col.gameObject); //�浹�� ������Ʈ ����(asteroid)
            Debug.Log($"�浹 :");
            var eff = Instantiate(effect, col.transform.position, Quaternion.identity);
            Destroy(eff, 1f); //����Ʈ ����
            GameManager.Instance.TurnOn(); //ī�޶� ��鸲
            GameManager.Instance.RoketHealtPoint(50); //ü�� ����
            source.PlayOneShot(hitClip, 1f); //�Ҹ� ���
                                             // �Ҹ��� �ѹ��� ����� (������?, ����);

            OnShake();
        }

    }
    public void Fire()
    {
        GameObject coin = PoolingManager.p_Instance.GetCoin();
        if (coin != null)
        {
            coin.transform.position = firePos.position; // �Ѿ� ��ġ ����
            coin.transform.rotation = firePos.rotation; // �Ѿ� ȸ�� ����
            coin.SetActive(true);
        }

        //�Ҹ��Լ�(What?, time?)
    }

    public void OnStickPos(Vector3 stickPos)
    {
        h = stickPos.x; v = stickPos.y;
    }
}
