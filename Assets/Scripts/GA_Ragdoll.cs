using System.Collections.Generic;
using UnityEngine;


public class GA_Ragdoll : MonoBehaviour
{
    public bool isRagdoll;
    private Animator animator;
    private Rigidbody mainRigidbody;
    private Collider mainCollider;
    private List<Rigidbody> childRigidbodies;
    private List<Collider> childColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Fill our variables
        animator = GetComponent<Animator>();
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>(); 

        childRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        childRigidbodies.Remove(mainRigidbody);

        childColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        childColliders.Remove(mainCollider);

        // Start based on flag
        if (isRagdoll)
        {
            EnableRagdoll();
        } else
        {
            DisableRagdoll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableRagdoll()
    {
        // Turn ON physics for all the child rigidbodies - isKinematic = false
        foreach (Rigidbody rb in childRigidbodies) 
        {
            rb.isKinematic = false;
        }

        // Turn ON collision for all the child colliders
        foreach (Collider col in childColliders)
        {
            col.enabled = true;
        }

        // Turn OFF physics for the main rigidbody - isKinematic = true
        mainRigidbody.isKinematic = true;
        // Turn OFF collision for main collider
        mainCollider.enabled = false;
        // Turn OFF the animator
        animator.enabled = false;

        // Set our flag
        isRagdoll = true;
    }
     
    public void DisableRagdoll()
    {
        // Turn OFF physics for all the child rigidbodies - isKinematic = true
        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = true;
        }
        // Turn OFF collision for all the child colliders
        foreach (Collider col in childColliders)
        {
            col.enabled = false;
        }

        // Turn ON physics for the main rigidbody - isKinematic = false
        mainRigidbody.isKinematic = false;

        // Turn ON collision for main collider
        mainCollider.enabled = true;

        // Turn ON the animator
        animator.enabled = true;

        // Set our flag
        isRagdoll = false;
    }

    public void ToggleRagdoll()
    {
        // Check if ragdoll is enabled!
        if (isRagdoll)
        {
            DisableRagdoll();
        }
        else
        {
            EnableRagdoll();
        }
    }

}
