using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;

    public float _fallSpeedYDampingChangeTreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }

    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normYPanAmount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        for (int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                //set the current active camera
                currentCamera = _allVirtualCameras[i];

                //set the framing transposer
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        normYPanAmount = framingTransposer.m_YDamping;

        ChangeYOffsetInvoker(2);
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;

        } else
        {
            endDampAmount = normYPanAmount;
        }

        IsLerpingYDamping = false;

        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallYPanTime));
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

    }

    public void ChangeYOffsetInvoker(float newOffset)
    {
        StartCoroutine(ChangeYOffset(newOffset));
    }
    
    private IEnumerator ChangeYOffset(float newOffset)
    {
        float currentOffset = currentCamera.GetComponent<CinemachineCameraOffset>().m_Offset.y;

        float deltaOffset = ((newOffset - currentOffset) / 0.2f) * 0.001f;

        while ((int)currentOffset != (int)newOffset)
        {
            currentOffset += deltaOffset;
            currentCamera.GetComponent<CinemachineCameraOffset>().m_Offset.y += deltaOffset;    

            yield return new WaitForSeconds(0.001f);
        }
    }

} 
