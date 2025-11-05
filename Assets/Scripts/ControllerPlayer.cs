using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public InputActionReference move;

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
        Vector2 moveVector = move.ToInputAction().ReadValue<Vector2>(); 
        pawn.Move(moveVector);
    }
}
