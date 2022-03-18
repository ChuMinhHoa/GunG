using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCinemachineController : MonoBehaviour
{
    public static CameraCinemachineController instance;

    public CinemachineVirtualCamera myCamera;

    [Range(0,1)]
    public float timeShake;

    public CinemachineBasicMultiChannelPerlin cinemachineBMP;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    private void Start()
    {
        cinemachineBMP = myCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CameraShake(float intensity) {
        cinemachineBMP.m_AmplitudeGain = intensity;
        StartCoroutine(ResetCameraShake());
    }

    IEnumerator ResetCameraShake() {
        yield return new WaitForSeconds(timeShake);
        cinemachineBMP.m_AmplitudeGain = 0;
    }
}
