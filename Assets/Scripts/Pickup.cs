using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    private new Collider collider;
    public virtual void Start()
    {
        // Get our collider, make sure it's a trigger
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

    }
    public abstract void OnTriggerEnter(Collider other);
}
