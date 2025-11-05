using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

[RequireComponent (typeof(Animator))]
public class PawnRootMotion : Pawn
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move(Vector2 moveVector)
    {
        // Move vector will send in the intended veclocity in each direction (as a vector)
        // Move Vector values represent percentage of stick movement -- values of 0 to 1 
        // Pass that to the animator, so the animator makes our character move
        moveVector = transform.InverseTransformDirection(moveVector);
        animator.SetFloat("Right", moveVector.x * moveSpeed);
        animator.SetFloat("Forward", moveVector.y * moveSpeed);
    }
    public override void Move(Vector3 moveVector)
    {
        Move(new Vector2(moveVector.x, moveVector.z));
    }

    public override void Rotate(float rotationDirection)
    {
        transform.Rotate(0.0f, rotationDirection * rotationSpeed * Time.deltaTime, 0.0f);
    }

    public override void RotateTowardsPoint(Vector3 pointToRotateTowards)
    {
        Vector3 targetDirection;
        targetDirection = pointToRotateTowards - transform.position;
        RotateTowardsDirection(targetDirection);
        
    }

    public override void RotateTowardsDirection(Vector3 directionToRotateTowards)
    {
       Quaternion targetRotation = Quaternion.LookRotation(directionToRotateTowards, Vector3.up); 
       transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnAnimatorMove()
    {
        // After the animation runs
        // Use root motion to move the game object
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;

        // If we have a NavMeshAgent on our controller,
        ControllerAI aiController = controller as ControllerAI;
        if ( aiController != null)
        {
            // Set our navMeshAgent to understand it is at the position from the animator
            aiController.agent.nextPosition = animator.rootPosition;
        }
    }
}
