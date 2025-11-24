using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Projectile : MonoBehaviour
{
    public float projecileSpeed = 10.0f;
    public float damageDone = 1.0f;
    public float lifespan = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        // Die after lifespan ends
        Destroy(gameObject, lifespan);

        // Make sure our collider is a trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void Move();
}
