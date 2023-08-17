using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleLightController : MonoBehaviour
{
    [SerializeField] private float _outerRadiusMinimum;
    [SerializeField] private float _outerRadiusMaximum;

    [SerializeField] private float _innerRadiusMinimum;
    [SerializeField] private float _innerRadiusMaximum;

    [SerializeField] private float _intensityMinimum;
    [SerializeField] private float _intensityMaximum;


    private Light2D light2D;

    private void Start()
    {
       light2D = GetComponent<Light2D>();
        StartCoroutine(LightStatsControl());
    }

    private IEnumerator LightStatsControl()
    {
        while (true)
        {
            light2D.pointLightOuterRadius = Random.Range(_outerRadiusMinimum, _outerRadiusMaximum);
            light2D.pointLightInnerRadius = Random.Range(_innerRadiusMinimum, _innerRadiusMaximum);
            light2D.intensity = Random.Range(_intensityMinimum, _intensityMaximum);

            yield return new WaitForSeconds(Random.Range(0.08f, 0.1f));
        }
    }
}
