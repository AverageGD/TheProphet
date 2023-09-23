using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateController : MonoBehaviour
{
    [Header("Particle System")]
    public ParticleSystem weatherParticles;

    [Header("Shape Settings")]
    [Range(1f, 300f)]
    public float shapeScaleX;

    [Header("Collision Settings")]
    public LayerMask collidesWith;

    private ParticleSystem.ShapeModule shapeModule;
    private ParticleSystem.CollisionModule collisionModule;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shapeModule = weatherParticles.shape;
        collisionModule = weatherParticles.collision;
        Music();
    }

    private void Update()
    {
        UpdateShapeSettings();
        UpdateCollisionSettings();
    }

    public void UpdateShapeSettings()
    {
        shapeModule.scale = new Vector3(shapeScaleX, shapeModule.scale.y, shapeModule.scale.z);
    }

    private void UpdateCollisionSettings()
    {
        collisionModule.collidesWith = collidesWith;
    }

    private void Music()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}