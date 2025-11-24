using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Data")]
    public float maxHealth;
    public float currentHealth;
    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage( float damageToTake )
    {
        // Subtract from health
        currentHealth -= damageToTake;

        // Check for death
        if ( currentHealth < 0)
        {
            currentHealth = 0;
            OnDeath.Invoke();
        }

        // Do anything in the OnTakeDamageEvent
        OnTakeDamage.Invoke();
    }
}
