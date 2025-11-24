using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    public Controller controller;
    public abstract void Move(Vector2 moveVector);
    public abstract void Move(Vector3 moveVector);
    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();

    public abstract void Rotate(float rotationDirection);
    public abstract void RotateTowardsPoint(Vector3 pointToRotateTowards);
    public abstract void RotateTowardsDirection(Vector3 directionToRotateTowards);
}
