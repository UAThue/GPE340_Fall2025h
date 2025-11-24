using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Animator))]
public class PawnRootMotion : Pawn
{
    private Animator animator;
    [HideInInspector] public Weapon weapon;
    [Tooltip("Make sure this is a prefab and not in the scene!")]
    public Transform weaponMountPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get our animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PullTrigger()
    { 
        if (weapon != null) 
        {
            weapon.OnTriggerPull.Invoke();
        }
    }

    public override void ReleaseTrigger()
    {
        if (weapon != null)
        {
            weapon.OnTriggerRelease.Invoke();
        }
    }

    public void EquipWeapon ( Weapon weaponToEquip )
    {
        // If we have a weapon already, unequip it
        if (weapon != null)
        {
            UnequipWeapon();
        }

        // Instantiate the weapon at a weapon mount point and rotation of mount point
        GameObject weaponObject = Instantiate(weaponToEquip.gameObject, weaponMountPoint.position, weaponMountPoint.rotation);

        // Parent the weapon to the mount point, so it moves with it
        weaponObject.transform.parent = weaponMountPoint;

        // Save our weapon
        weapon = weaponObject.GetComponent<Weapon>();

    }


    public void UnequipWeapon ( )
    {
        // Destroy our weapon's game object
        Destroy(weapon.gameObject);

        // Make sure our weapon is set to null
        weapon = null;
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

    public void OnAnimatorIK()
    {
        if (weapon != null)
        {
            if (weapon.RHPoint != null)
            {
                animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RHPoint.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RHPoint.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            } else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
            }
            if (weapon.LHPoint != null)
            {
                animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LHPoint.position);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LHPoint.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            } else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
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
