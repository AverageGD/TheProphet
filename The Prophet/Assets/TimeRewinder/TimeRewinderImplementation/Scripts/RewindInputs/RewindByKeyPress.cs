using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  Example how to rewind time with key press
/// </summary>
public class RewindByKeyPress : MonoBehaviour
{
    bool isRewinding = false;
    [SerializeField] float rewindIntensity = 0.02f;          //Variable to change rewind speed
    float rewindValue = 0;

    public UnityEvent OnRewind;
    public UnityEvent OnRewindEnd;

    bool isUsed = false;

    public void RewindInvoker()
    {
        if (!isRewinding && !isUsed)
            StartCoroutine(RewindAction());
    }
    private IEnumerator RewindAction()
    {
        rewindValue += rewindIntensity;

        RewindManager.Instance.StartRewindTimeBySeconds(rewindValue);

        while (rewindValue <= Mathf.Min(RewindManager.Instance.HowManySecondsAvailableForRewind - 1, Time.fixedTime))
        {

            RewindManager.Instance.SetTimeSecondsInRewind(rewindValue);
            OnRewind?.Invoke();
            isRewinding = true;

            yield return new WaitForFixedUpdate();

            rewindValue += rewindIntensity;
        }

        RewindManager.Instance.StopRewindTimeBySeconds();
        rewindValue = 0;
        isRewinding = false;
        OnRewindEnd?.Invoke();
        isUsed = true;
    }


}
