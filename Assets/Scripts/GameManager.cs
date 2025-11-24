using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    [Header("Important Objects")]
    [Tooltip("Up to date list of all our players.")] public List<ControllerPlayer> players;
    [Tooltip("Up to date list of all our AIs.")] public List<ControllerAI> ais;
    [Tooltip("Up to date list of all our PlayerSpawns.")] public List<PlayerSpawn> playerSpawns;

    [Header("Prefabs")]
    public ControllerPlayer pf_playerControler;
    public Pawn pf_playerPawn;

    [Header("Level/Wave Data")]
    public int currentLevel;


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
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void SpawnPlayer()
    {
        // TODO: Spawn a player (controller and pawn)
        ControllerPlayer newPlayerController = Instantiate<ControllerPlayer>(pf_playerControler, Vector3.zero, Quaternion.identity);
        Pawn newPlayerPawn = Instantiate<Pawn>(pf_playerPawn);

        // TODO: Possess the player pawn
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
