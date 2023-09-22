using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    public float volume = 0.01f;
    public float pitch = 1f;

    public List<AudioClip> audioClips = new List<AudioClip>();
}
