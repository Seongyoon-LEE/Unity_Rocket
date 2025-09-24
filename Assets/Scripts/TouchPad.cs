using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventTrigger))] //��Ʈ����Ʈ atrribute
public class TouchPad : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��ġ�е�")] private RectTransform touchPad;//�е� ��Ʈ Ʈ������
    [SerializeField] private Vector3 _StartPos; //���� ���� ��ġ 
    [SerializeField] private float _dragRadius = 120f; //������ �� ��ġ�� ���߿� �˾Ƽ� ��ġ��
    [SerializeField] private Rocket _rocket; //�е��� ���Ⱚ�� �÷��̾ ���� �ϱ� ���� 
    private bool isPressed = false; //��ư�� �������� ���� 
    private int _touchId = -1; // ���콺 ����Ʈ�� �հ����� ���ȿ� �ִ� �� üũ�� �ϱ� ���� 

    void Start()
    {
        touchPad = GetComponent<RectTransform>();
        // _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        _StartPos = touchPad.position;
    }
    public void ButtonDown()
    {
        isPressed = true;
    }
    public void ButtonUp()
    {
        isPressed = false;
        HandleInput(_StartPos);
    }

    void FixedUpdate() //���������� ��Ȯ�� �������� ���� ���� ���� �Ѵٸ� FixedUpdate�� �Ѵ�
    {                  // ��Ȯ�� �ð��� ���� ���� ���� �Ѵٸ� FixedUpdate�� �Ѵ�

        //���⼭ ���̽�ƽ �е��� ������ �÷��̾�� FixedUpdate() ���߾ ���� 
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                HandleTouchInput();

                break;

            case RuntimePlatform.IPhonePlayer:
                HandleTouchInput();

                break;
            case RuntimePlatform.WindowsEditor:

                HandleInput(Input.mousePosition);

                break;

        }


    }
    void HandleTouchInput() //����� �ȵ���̵� IOS
    {
        int i = 0;
        if (Input.touchCount > 0) //�ѹ��̶� ��ġ�� �Ǿ�ٸ�
        {
            //��ġ ���� ���� ��ġ�� ������ touches��� �迭 ������Ƽ�� �����ϰ� ����
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                //��ġ�� ���� �Ǿ��� �� 
                if (touch.phase == TouchPhase.Began)
                {         //������ ����� �ʾҴٸ� 
                    if (touch.position.x <= (_StartPos.x + _dragRadius))
                        _touchId = i;

                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {     //��ġ ������ �е带 �����̰� �ְų�   ���߾��� �ִٸ� 

                    if (_touchId == i) //���ȿ� �ִٸ� 
                    {
                        HandleInput(touchPos); //������ �е带 �����̴� �޼���
                    }

                }
                if (touch.phase == TouchPhase.Ended)
                {   //��ġ�� �����ٸ� �ٽ�  _touchId�� -1�� �ٲپ���
                    if (_touchId == i)
                        _touchId = -1;
                }
            }

        }



    }
    void HandleInput(Vector3 input) //windowEidtor
    {
        if (isPressed)
        {
            //Vector3 differVector1 = (input - _StartPos).normalized;
            Vector3 differVector = (input - _StartPos);
            //��ü ����  �Ÿ��� ũ��  ������ ���� ���ٸ� 
            if (differVector.sqrMagnitude > (_dragRadius * _dragRadius))
            {         //�ٻ簪 ��ü ũ�� 
                differVector.Normalize(); //������ ���� �ϰ� 
                touchPad.position = _StartPos + differVector * _dragRadius;
                //������ ������� 

            }
            else
            {
                touchPad.position = input;
            }

        }
        else
        {
            touchPad.position = _StartPos;
        }
        //���� �������� (if���̰� else�̰� ����) �Ÿ��� ���ϰ� 
        Vector3 differ = touchPad.position - _StartPos;
        Vector3 normalDiffer = new Vector3(differ.x / _dragRadius, differ.y / _dragRadius);
        //�Ÿ�X / ������  = ������ ���Ѵ�. 
        if (_rocket != null)
        {
            //�÷��̾� ���� ������ ���� ������ ����
            _rocket.OnStickPos(normalDiffer);
        }


    }
}