using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PawnRootMotion : Pawn
{
    private Animator animator;
    public float moveSpeed;
    public float rotationSpeed;

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
        animator.SetFloat("Right", moveVector.x * moveSpeed);
        animator.SetFloat("Forward", moveVector.y * moveSpeed);
    }
    public override void Move(Vector3 moveVector)
    {
        Move(new Vector2(moveVector.x, moveVector.z));
    }

}
