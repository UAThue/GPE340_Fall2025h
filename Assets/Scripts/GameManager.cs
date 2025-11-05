using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Pawn playerPawn;
    public List<Controller> players;


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
}
