using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;


public class CameraShake : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vc;
    [SerializeField]
    CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
        Rocket.OnShake += this.ShakeCamera;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopCameraShake();
    }
    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = 5f;
        noise.m_FrequencyGain = 3f;
        Invoke("StopCameraShake", 1.5f);

    }
    public void StopCameraShake()
    {
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }

    void Update()
    {

    }
}
