using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    
    public void GameStart()
    {
        
        SceneManager.LoadScene("PlayScene"); // 씬 

    }
    public void GameQuit()
    {
#if UNITY_EDITOR // 매크로 지정 #if :  컴파일 이전에 기능을 만들어 놓는것 
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 게임 종료
#else
        Application.Quit(); // 빌드한 exe 파일이 꺼질뿐 유니티에선 안사라짐 게임종료
#endif
    }
}
