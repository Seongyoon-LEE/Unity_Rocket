using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float h = 0f; //A,D Ű�� �ޱ� ���� ���� 
    private float v = 0f; //W,S Ű�� �ޱ� ���� ���� 
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source; // ������ҽ�
    public AudioClip hitclip; // �����Ŭ��
    private Vector2 StartPos = Vector2.zero; // ������ġ
    public Transform firePos; // �߻� ��ġ
    public GameObject coinPrefab; // ���� ������
    
    

    void Start()
    {
        source = GetComponent<AudioSource>();
        
        tr = GetComponent<Transform>();
    }

    
    void Update()
    {
        if (GameManager.Instance.isGameOver == true)
            return;
        if(Application.platform == RuntimePlatform.WindowsEditor) // ������ �÷����϶�
        {
            Window();
        }

        if (Application.platform == RuntimePlatform.Android) // �ȵ���̵� �÷����϶�
        {
            Mobile();
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer) // ������ �÷����϶�
        {
            Mobile();
        }

        #region �ʺ��� ����
        //����ȭ
        //tr.Translate(Vector3.up * v * speed * Time.deltaTime);
        //if (tr.position.x > 7.6f)
        //    tr.position = new Vector3(7.6f, tr.position.y, tr.position.z);
        //else if(tr.position.x <= -7.8f)
        //    tr.position = new Vector3(-7.8f,tr.position.y, tr.position.z);

        //if (tr.position.y > 4.18f)
        //    tr.position = new Vector3(tr.position.x, 4.18f, tr.position.z);
        //else if (tr.position.y <= -4.2f)
        //    tr.position = new Vector3(tr.position.x, -4.2f, tr.position.z);
        #endregion
        #region �߱��� ����
        tr.position = new Vector3(Mathf.Clamp(tr.position.x,-7f,7f), Mathf.Clamp(tr.position.y,-4.5f,4.5f),tr.position.z);
                                //���� Ŭ����(Mathf.Clamp(what?,min,max) // ���� �����ϴ� �Լ�                
        #endregion

    }

    private void Window()
    {
        h = Input.GetAxis("Horizontal");//A,DŰ�� �ޱ� ���� ����
        v = Input.GetAxis("Vertical");//W,SŰ�� �ޱ� ���� ����
        Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
        tr.Translate(Normal.normalized * speed * Time.deltaTime);
    }

    private void Mobile()
    {
        if (Input.touchCount > 0) // �ѹ��̶� ��ġ�� �Ǿ��ٸ� 
        {
            Touch touch = Input.GetTouch(0); // ��ġ�� ������ ������ 
                                             //GetTouch(0) // ��ġ�� ��ġ���� �迭�� ���� �Ǿ��µ� ù��° ��ġ�� ��ġ�� ������
            switch (touch.phase) // ��ġ�� ���¸� ������
            {
                case TouchPhase.Began: // ��ġ�� ���۵Ǿ�����
                    StartPos = touch.position; // ��ġ�� ��ġ�� ����
                    break;
                case TouchPhase.Moved: // ��ġ�� �̵�������
                    Vector3 touchDelta = touch.position - StartPos; // ��ġ�� ��ġ - ���� ��ġ �Ÿ��� ���� 
                    Vector3 moveDir = new Vector3(touchDelta.x, touchDelta.y, 0); // ��ġ�� ��ġ - ���� ��ġ �Ÿ�
                    tr.Translate(moveDir.normalized * speed * Time.deltaTime); // ��ġ�� ��ġ - ������ġ �Ÿ�
                    StartPos = touch.position; // ��ġ�� ��ġ�� ���� 
                    break;

            }
        }
    }

    // trigger �浹 ó�� {�ݹ� �Լ� : ������ ȣ��Ǵ� �Լ�}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="Asteroid")
        {
            
            
            Destroy(col.gameObject); // �浹�� ������Ʈ(Asteroid) ����
            Debug.Log("�浹");
            var eff = Instantiate(effect,col.transform.position,Quaternion.identity);
            Destroy(eff,1f); // ����Ʈ ����
            GameManager.Instance.TurnOn(); // ī�޶� ��鸲
            GameManager.Instance.RoketHealtPoint(100); // ü�� ����
            source.PlayOneShot(hitclip, 1f); //�Ҹ� ���
            // �Ҹ��� �ѹ��� ����� (������?,����)
        }

    }

    public void Fire()
    {
        //������ �����Լ�(What? Where? How rotation?)
        GameObject coin = Instantiate(coinPrefab, firePos.position, Quaternion.identity); // ���λ���
        //Destroy(coin, 1.5f); // ���� ����B
    }
}
