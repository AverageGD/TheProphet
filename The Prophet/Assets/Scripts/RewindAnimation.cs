using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class RewindAnimation : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;

    [Header("Effect Settings")]
    public float distortionSpeed = 4f;
    public float grayscaleSpeed = 1f;
    public float distortionIntensity = -0.6f;
    public float duration = 15f; 

    private bool isRewindUsed = false;
    private bool isAnimating = false;

    void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments) && volume.profile.TryGet(out lensDistortion))
        {
            colorAdjustments.saturation.value = 0;
            lensDistortion.intensity.value = 0;
        }
    }

    public void ActivateRewindAnimationInvoker()
    {
        if (!isAnimating && !isRewindUsed)
            StartCoroutine(ActivateRewindAnimation());
    }

    public void DeactivateRewindAnimationInvoker()
    {
        if (!isAnimating && isRewindUsed)
            StartCoroutine(DeactivateRewindAnimation());
    }

    IEnumerator ActivateRewindAnimation()
    {
        isAnimating = true;


        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * distortionSpeed;
            lensDistortion.intensity.value = Mathf.Lerp(0, distortionIntensity, Mathf.Sin(t * Mathf.PI));
            t += Time.deltaTime * grayscaleSpeed;
            colorAdjustments.saturation.value = Mathf.Lerp(0, -100, t);
            yield return null;
        }


        lensDistortion.intensity.value = 0;
        isRewindUsed = true;
        isAnimating = false;


        yield return new WaitForSeconds(duration);


    }

    IEnumerator DeactivateRewindAnimation()
    {
        isAnimating = true;


        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * grayscaleSpeed;
            colorAdjustments.saturation.value = Mathf.Lerp(-100, 0, t);
            t += Time.deltaTime * distortionSpeed;
            lensDistortion.intensity.value = Mathf.Lerp(0, 0.6f, Mathf.Sin(t * Mathf.PI));

            yield return null;
        }


        lensDistortion.intensity.value = 0;
        isRewindUsed = false;
        isAnimating = false;
    }
}
