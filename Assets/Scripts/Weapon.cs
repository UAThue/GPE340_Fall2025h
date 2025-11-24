using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    public float damageDone;
    public float fireRate;
    public int ammoCount = 100;
    public int maxAmmo = 100;

    [Header("Events")]
    public UnityEvent OnTriggerPull;
    public UnityEvent OnTriggerRelease;
    public UnityEvent OnReload;
    public UnityEvent OnOutOfAmmo;

    [Header("Transforms")]
    public Transform RHPoint;
    public Transform LHPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
