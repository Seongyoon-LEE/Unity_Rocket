using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    
    public void GameStart()
    {
        
        SceneManager.LoadScene("PlayScene"); // �� 

    }
    public void GameQuit()
    {
#if UNITY_EDITOR // ��ũ�� ���� #if :  ������ ������ ����� ����� ���°� 
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ����
#else
        Application.Quit(); // ������ exe ������ ������ ����Ƽ���� �Ȼ���� ��������
#endif
    }
}
