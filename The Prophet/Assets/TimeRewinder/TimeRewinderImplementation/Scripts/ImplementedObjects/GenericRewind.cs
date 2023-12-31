﻿using UnityEngine;
using UnityEngine.Events;

public class GenericRewind : RewindAbstract
{
    [Tooltip("Tracking active state of the object that this script is attached to")]
    [SerializeField] bool trackObjectActiveState;
    [Tooltip("Tracking Position,Rotation and Scale")]
    [SerializeField] bool trackTransform;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackHealth;
    [SerializeField] bool trackSpriteRenderer;
    [SerializeField] bool trackRoomID;

    [Tooltip("Enable checkbox on right side to track particles")]
    [SerializeField] OptionalParticleSettings trackParticles;

    public UnityEvent OnRewind;
    public UnityEvent OnTrack;

    public override void Rewind(float seconds)
    {
        OnRewind?.Invoke();

        if (gameObject == null)
            return;

        if (trackObjectActiveState)
            RestoreObjectActiveState(seconds);
        if (trackTransform)
            RestoreTransform(seconds);
        if (trackVelocity)
            RestoreVelocity(seconds);
        if (trackAnimator)
            RestoreAnimator(seconds);
        if (trackAudio)
            RestoreAudio(seconds);
        if (trackParticles.Enabled)
            RestoreParticles(seconds);   
        if (trackHealth)
            RestoreHealthSystem(seconds);
        if (trackSpriteRenderer)
            RestoreFlips(seconds);
        if (trackRoomID)
            RestoreRoomID(seconds);
    }

    public override void Track()
    {
        OnTrack?.Invoke();

        if (gameObject == null)
            return;

        if (trackObjectActiveState)
            TrackObjectActiveState();
        if (trackTransform)
            TrackTransform();
        if (trackVelocity)
            TrackVelocity();
        if (trackAnimator)
            TrackAnimator();
        if (trackAudio)
            TrackAudio();
        if (trackParticles.Enabled)
            TrackParticles();
        if (trackHealth)
            TrackHealthSystem();
        if (trackSpriteRenderer)
            TrackFlips();
        if (trackRoomID)
            TrackRoomID();
    }
    private void Start()
    {
        if(trackParticles.Enabled)
            InitializeParticles(trackParticles.Value);
    }
}

