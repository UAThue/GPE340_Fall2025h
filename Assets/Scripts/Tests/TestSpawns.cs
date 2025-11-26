using UnityEngine;

public class TestSpawns : MonoBehaviour
{
    public Pawn pf_EnemyPawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (GameManager.instance.currentWave < GameManager.instance.waves.Count)
            {
                GameManager.instance.SpawnWave(GameManager.instance.waves[GameManager.instance.currentWave]);
                GameManager.instance.currentWave++;
            }
        }

    }
}
