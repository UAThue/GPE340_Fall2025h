using UnityEngine;


[RequireComponent(typeof(Health))]
    public abstract class Death : MonoBehaviour
{
    public abstract void Die();
    public virtual void Start() 
    {
        Health healthComponent = GetComponent<Health>();
        healthComponent.OnDeath.AddListener(Die);
    }
}
