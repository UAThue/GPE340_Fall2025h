using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ControllerAI : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;
    private Transform targetTransform;
    public Weapon startingWeapon;
    public float shootDistance = 5.0f;
    public float shootAngle = 45.0f;


    public override void Start()
    {
        // Equip weapon
        // Typecast (with null if cast failed)
        if (startingWeapon != null)
        {
            PawnRootMotion prmPawn = pawn as PawnRootMotion;
            if (prmPawn != null)
            {
                prmPawn.EquipWeapon(startingWeapon);
            }
        }

        if (GameManager.instance != null) 
        {
            GameManager.instance.ais.Add(this);
        }

        // Do what all controllers do on start
        base.Start();
    }

    public override void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ais.Remove(this);
        }
    }

    void Update()
    {
        // Make sure they have a target
        if (targetTransform != null)
        {

            SeekTarget();
            // If withing our shooting parameters
            if (IsTargetInShootingParameters())
            {                 
                pawn.PullTrigger();
            } else
            {
                pawn.ReleaseTrigger();
            }
        } else
        {
            TargetPlayerPawn();
        }
    }

    private bool IsTargetInShootingParameters()
    {
        // Check distance
        if (Vector3.Distance(pawn.transform.position, targetTransform.position) < shootDistance)
        {
            // Check angle
            if (Vector3.Angle(pawn.transform.forward, GetVectorToTarget()) < shootAngle)
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 GetVectorToTarget()
    {
        // To get a vector between two points: End minus start 
        return targetTransform.position - pawn.transform.position;
    }

    private void SeekTarget()
    {
        // Make the decisions
        agent.SetDestination(targetTransform.position);

        // Set our speeds
        agent.angularSpeed = pawn.rotationSpeed;
        agent.speed = pawn.moveSpeed;

        // Send the target velocity to our move command in our pawn
        pawn.Move(agent.desiredVelocity);

        // Rotate to look at our target transform
        pawn.RotateTowardsPoint(targetTransform.position);
    }


    private void TargetPlayerPawn()
    {
        ControllerPlayer playerController = FindAnyObjectByType<ControllerPlayer>();
        if (playerController != null)
        {
            if (playerController.pawn != null)
            {
                targetTransform = playerController.pawn.transform;
            }
            else
            {
                Debug.LogWarning("WARNING: Player controller does not have a pawn.");
                targetTransform = null;
            }
        }
        else
        {
            Debug.LogWarning("WARNING: No player controllers in scene.");
            targetTransform = null;
        }
    }


    public override void Possess(Pawn pawnToPossess)
    {
        // Set variables for pawn
        base.Possess(pawnToPossess);

        // Get the agent from the pawn
        agent = pawnToPossess.GetComponent<NavMeshAgent>();
        // If there is not one, add one
        if (agent == null)
        {
            agent = pawnToPossess.AddComponent<NavMeshAgent>();
        }

        // Set the stopping distance of the pawn
        agent.stoppingDistance = stoppingDistance;

        // Disable movement and rotation from the NavMeshAgent
        agent.updatePosition = false;
        agent.updateRotation = false;
    }


    public override void Unpossess()
    {
        // Destroy the nav mesh agent component
        if (agent != null)
        {
            Destroy(agent);
        }

        // Set variable to null from parent class
        base.Unpossess();
    }




}
