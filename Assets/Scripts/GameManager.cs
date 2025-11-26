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
    [Tooltip("Up to date list of all our EnemySpawns.")] public List<EnemySpawn> enemySpawns;
    public Camera playerCamera;

    [Header("Prefabs")]
    public ControllerPlayer pf_playerController;
    public ControllerAI pf_enemyController;
    public Pawn pf_playerPawn;
    public Pawn pf_enemyPawn;

    [Header("GameData")]
    public bool isPaused;
    public int startingLives = 3;

    [Header("Level/Wave Data")]
    public int currentWave;
    public List<Wave> waves;



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
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {        
    }


    public void StartGame()
    {
        // Start at level 0
        currentWave = 0;        

        // Spawn the player
        SpawnPlayer();
    }



    public void SpawnWave(Wave waveToSpawn)
    {
        // For each element in our wave
        foreach (WaveElement element in waveToSpawn.spawns)
        {
            // "Count" number of times
            for (int i = 0; i < element.count; i++)
            {
                // Spawn the enemy
                SpawnEnemy(element.enemyToSpawn);
            }
        }
    }

    public void SpawnPlayer()
    {
        ControllerPlayer newPlayerController;
        if (players.Count == 0)
        {
            // Spawn a player controller 0
            newPlayerController = Instantiate<ControllerPlayer>(pf_playerController, Vector3.zero, Quaternion.identity);
            /* if (!players.Contains(newPlayerController)) 
            {
                players.Add(newPlayerController);
            }*/
        }
        else
        {
            // Our new player controller is player 0
            newPlayerController = players[0];
        }

        // Now that we have a player 0, check if it has lives
        if (players[0].lives > 0)
        {
            // Spawn the pawn
            Pawn newPlayerPawn = Instantiate<Pawn>(pf_playerPawn);

            // Remove one life
            newPlayerController.lives--;

            // Possess the player pawn
            newPlayerController.Possess(newPlayerPawn);
        } else
        {
            // Load our Game Over Screen - we are out of lives!
            DoGameOver();            
        }
    }

    public void DoGameOver()
    {
        // TODO: Game over, man!
        Debug.Log("GAME OVER!");
    }

    public void SpawnEnemy(Pawn enemyPrefabToSpawn)
    {
        // Spawn an enemy controller
        ControllerAI newEnemyController= Instantiate<ControllerAI>(pf_enemyController, Vector3.zero, Quaternion.identity);

        // Spawn a pawn for the enemy
        EnemySpawn spawnPoint = GetRandomEnemySpawnPoint();
        Pawn newEnemyPawn = Instantiate<Pawn>(enemyPrefabToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);

        // Have the enemy controller possess the pawn
        newEnemyController.Possess(newEnemyPawn);

        // TODO: Anything else we need to do when an enemy is spawned
    }

    private EnemySpawn GetRandomEnemySpawnPoint()
    {
        return (enemySpawns[Random.Range(0, enemySpawns.Count)]);
    }

    public void Pause()
    {
        // Scale the delta time
        Time.timeScale = 0;

        // Set a paused boolean
        isPaused = true;
    }

    public void Unpause()
    {
        // Scale the delta time
        Time.timeScale = 1.0f;

        // Set a paused boolean
        isPaused = false;
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            Pause();
        } else
        {
            Unpause();
        }
    }
}
