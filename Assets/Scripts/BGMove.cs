using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    // �������� : ���� �̵�����, �ӵ�, ����
    private float x = 0f, y = 0f;
    public float speed = 5f;
    public MeshRenderer meshRenderer; // ��? why? 
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    
    void Update()
    {
        BackGroundmove();
        //�޽� ������ ���� ��Ƽ���� �ȿ� ���� �ؽ����� ������ = new Vector2(x,y)
    }

    private void BackGroundmove()
    {
        x += Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
    }
}
