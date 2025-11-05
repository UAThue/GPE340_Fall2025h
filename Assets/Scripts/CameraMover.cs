using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform objectToFollow;
    public float distanceFromObject = 10.0f;
    public float cameraMoveSpeed = 1.0f;
    public float cameraTurnSpeed = 180.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Teleport to the target position
        transform.position = objectToFollow.position + (distanceFromObject * Vector3.up);
        transform.LookAt(objectToFollow.position, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        // Move "a little bit" closer to our target position
        Vector3 targetPosition = objectToFollow.position + (distanceFromObject * Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);

        // Rotate a "little bit" closer to looking at our target
        Vector3 targetLookVector = objectToFollow.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetLookVector, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, cameraTurnSpeed * Time.deltaTime);
    }
}
