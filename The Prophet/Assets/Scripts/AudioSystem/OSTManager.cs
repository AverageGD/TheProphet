using System.Collections;
using UnityEngine;

public class OSTManager : MonoBehaviour
{
    [SerializeField] private AudioContainer _ambientContainer;

    private AudioSource audioSource;
    private float soundElapsedTime = 0f;


    public bool needAmbient = true;

    public static OSTManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _ambientContainer.audioClips[0];
    }

    private void Update()
    {
        if (needAmbient && soundElapsedTime > audioSource.clip.length)
        {
            soundElapsedTime = 0f;

            audioSource.clip = _ambientContainer.audioClips[Random.Range(0, _ambientContainer.audioClips.Count)];

            audioSource.volume = _ambientContainer.volume;
            audioSource.pitch = _ambientContainer.pitch;
            audioSource.Play();
        }

        if (!needAmbient)
            audioSource.Stop();

        soundElapsedTime += Time.deltaTime;
    }



}
