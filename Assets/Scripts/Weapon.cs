using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    public float damageDone;
    [Tooltip("Rate of Fire in Bullets/Second")] public float fireRate;
    public int ammoCount = 100;
    public int maxAmmo = 100;
    [Range(0.0f, 1.0f)] public float accuracy;
    [Tooltip("Maximum angle change due to poor accuracy")][Range(0.0f, 90.0f)] public float accuracyDelta;

    [Header("Events")]
    public UnityEvent OnTriggerPull;
    public UnityEvent OnTriggerRelease;
    public UnityEvent OnShoot;
    public UnityEvent OnReload;
    public UnityEvent OnOutOfAmmo;

    [Header("Transforms")]
    public Transform RHPoint;
    public Transform LHPoint;
    public Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetAccuracyModifiedRotationDegrees(float accuracyModifier)
    {
        // Choose a random percent of delta to be off
        // We use 1-accuracy so that an accuracy of 1 gives us a 0, and an accuracy of 0 gives us a 1. (Inverse percentage)
        float randomPercent = Random.Range(0.0f, 1 - (accuracy * accuracyModifier));

        // Get that percent of our delta
        float randomRotationDegrees = randomPercent * accuracyDelta;

        // 50/50 coin flip on which direction to misfire
        if (Random.value < 0.5f)
        {
            return randomRotationDegrees;
        }
        else
        {
            return -randomRotationDegrees;
        }
    }

    public float GetAccuracyModifiedRotationDegrees()
    {
        // If we don't tell the code a modifier for the controller/player/AI, then assume they have perfect accuracy
        return GetAccuracyModifiedRotationDegrees(1.0f);
    }
}
