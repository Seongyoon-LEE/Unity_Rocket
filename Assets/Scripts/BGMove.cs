using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    // 변수지정 : 어디로 이동할지, 속도, 방향
    private float x = 0f, y = 0f;
    public float speed = 5f;
    public MeshRenderer meshRenderer; // 왜? why? 
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    
    void Update()
    {
        BackGroundmove();
        //메쉬 렌더러 접근 머티리얼 안에 메인 텍스쳐의 오프셋 = new Vector2(x,y)
    }

    private void BackGroundmove()
    {
        x += Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
    }
}
