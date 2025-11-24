using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public Pawn playerPawn;
    [Tooltip("Up to date list of all our players.")] public List<Controller> players;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: Make this safe for errors - spawn player and controller when needed
        //       Right now, it requires the controller and pawn in the world and connected in the inspector
     
        // Have Player 0 possess a pawn
        players[0].Possess(playerPawn);
    }

    // Update is called once per frame
    void Update()
    {        
    }

    /// <summary>
    /// Makes the screen flash a defined number of times.
    /// Uses a coroutine that can be stopped  by calling StopAllCoroutines
    /// Does not make the object invulnerable while flashing; you are looking for the ImmuneFlash() function.
    /// </summary>
    /// <param name="numFlashes">Number of Times to flash the screen</param>
    public void FlashScreen ( int numFlashes)
    {
        // TODO: Make the screen flash
    }
}
