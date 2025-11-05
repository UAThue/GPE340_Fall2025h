using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ControllerAI : Controller
{
    public NavMeshAgent agent;
    public float stoppingDistance;
    public Transform targetTransform;

    void Update()
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
