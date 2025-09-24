using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventTrigger))] //애트리뷰트 atrribute
public class TouchPad : MonoBehaviour
{
    [SerializeField]
    [Tooltip("터치패드")] private RectTransform touchPad;//패드 랙트 트랜스폼
    [SerializeField] private Vector3 _StartPos; //시작 지점 위치 
    [SerializeField] private float _dragRadius = 120f; //반지름 이 수치는 나중에 알아서 고치길
    [SerializeField] private Rocket _rocket; //패드의 방향값을 플레이어에 전달 하기 위해 
    private bool isPressed = false; //버튼을 눌렀는지 여부 
    private int _touchId = -1; // 마우스 포인트나 손가락이 원안에 있는 지 체크를 하기 위해 

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

    void FixedUpdate() //고정프레임 정확한 물리량에 따른 것을 구현 한다면 FixedUpdate로 한다
    {                  // 정확한 시간에 따른 것을 구현 한다면 FixedUpdate로 한다

        //여기서 조이스틱 패드의 방향을 플레이어에의 FixedUpdate() 맞추어서 전달 
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
    void HandleTouchInput() //모바일 안드로이드 IOS
    {
        int i = 0;
        if (Input.touchCount > 0) //한번이라도 터치가 되어다면
        {
            //터치 했을 때의 위치와 방향을 touches라는 배열 프로퍼티가 저장하고 있음
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                //터치가 시작 되었을 때 
                if (touch.phase == TouchPhase.Began)
                {         //원밖을 벗어나지 않았다면 
                    if (touch.position.x <= (_StartPos.x + _dragRadius))
                        _touchId = i;

                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {     //터치 유형이 패드를 움직이고 있거나   멈추어져 있다면 

                    if (_touchId == i) //원안에 있다면 
                    {
                        HandleInput(touchPos); //실제로 패드를 움직이는 메서드
                    }

                }
                if (touch.phase == TouchPhase.Ended)
                {   //터치가 끝났다면 다시  _touchId를 -1로 바꾸어줌
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
            //전체 원의  거리의 크기  원밖을 벗어 났다면 
            if (differVector.sqrMagnitude > (_dragRadius * _dragRadius))
            {         //근사값 전체 크기 
                differVector.Normalize(); //방향을 유지 하고 
                touchPad.position = _StartPos + differVector * _dragRadius;
                //원밖을 못벗어나게 

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
        //위의 로직들의 (if문이건 else이건 간에) 거리를 구하고 
        Vector3 differ = touchPad.position - _StartPos;
        Vector3 normalDiffer = new Vector3(differ.x / _dragRadius, differ.y / _dragRadius);
        //거리X / 반지름  = 방향을 구한다. 
        if (_rocket != null)
        {
            //플레이어 한테 위에서 구한 방향을 전달
            _rocket.OnStickPos(normalDiffer);
        }


    }
}