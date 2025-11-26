using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    [Header("Input")]
    public InputActionReference move;
    public bool isMouseRotation = true;
    public Camera inputCamera;
    [Header("Lives")]
    public int lives = 3;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        Debug.Log("Added to List");
        // Add self to list of players
        if (GameManager.instance != null)
        {
            GameManager.instance.players.Add(this);
        }
    }

    public override void Start()
    {
        // Set lives to default from GameManager
        lives = GameManager.instance.startingLives;

        // Start score at 0
        score = 0;

        // Do what all controllers need to do
        base.Start();
    }

    public override void OnDestroy() {
        if (GameManager.instance != null)
        {
            GameManager.instance.players.Remove(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Quit early if we are paused!
        if (GameManager.instance.isPaused) return;

        // Otherwise...
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

    public override void Possess(Pawn pawnToPossess)
    {
        // Take control of the pawn
        base.Possess(pawnToPossess);

        // Set our camera to read mouse position (in mouse rotation code)
        inputCamera = GameManager.instance.playerCamera;

        // Set our camera to follow the pawn
        CameraMover cameraMover = inputCamera.GetComponent<CameraMover>();        
        cameraMover.objectToFollow = pawn.transform;
    }
}
