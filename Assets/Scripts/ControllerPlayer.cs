using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public InputActionReference move;
    public bool isMouseRotation = true;
    public Camera inputCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProccessInputs();
    }

    private void ProccessInputs()
    {
        // Move Vector values represent percentage of stick movement -- values of 0 to 1 
        Vector2 moveVector2 = move.ToInputAction().ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(moveVector2.x, 0.0f, moveVector2.y);
        //moveVector = pawn.transform.InverseTransformDirection(moveVector);
        pawn.Move(moveVector);

        // If we are using mouse rotation, rotate to look at mouse
        if (isMouseRotation )
        {
            RotateToLookAtMouse();
        }    

        // Handle trigger pulls
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pawn.PullTrigger();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            pawn.ReleaseTrigger();
        }
    }

    private void RotateToLookAtMouse()
    {
        Plane footPlane = new Plane(Vector3.up, pawn.transform.position);
        Ray pointerRay = inputCamera.ScreenPointToRay(Input.mousePosition);

        float distanceToIntersection;
        if (footPlane.Raycast(pointerRay, out distanceToIntersection))
        {
            Vector3 raycastHitPoint = pointerRay.GetPoint(distanceToIntersection);
            pawn.RotateTowardsPoint(raycastHitPoint);
        } else
        {
            Debug.LogWarning("WARNING: Camera not looking at the ground plane. Cannot rotate.");
        }
    }
}
