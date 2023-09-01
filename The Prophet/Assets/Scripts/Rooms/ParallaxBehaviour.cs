using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField] private float _parallaxStrength;
    [SerializeField] private bool _disableVerticalParallax;
    [SerializeField] private bool _disableHorizontalParallax;

    private Vector3 targetPreviousPosition;

    private void Start()
    {

        if (!_followingTarget)
        {
            _followingTarget = Camera.main.transform;
        }

        targetPreviousPosition = _followingTarget.position;
    }

    private void Update()
    {
        Vector3 deltaDistance = _followingTarget.position - targetPreviousPosition;

        if (_disableVerticalParallax)
            deltaDistance.y = 0;

        if (_disableHorizontalParallax)
            deltaDistance.x = 0;

        targetPreviousPosition = _followingTarget.position;

        transform.position += deltaDistance * _parallaxStrength;
    }

}
