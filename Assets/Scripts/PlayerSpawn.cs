using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerSpawns.Add(this);
        }
    }

    void OnDestroy()
    {
        if (GameManager.instance != null )
        {
            GameManager.instance.playerSpawns.Remove(this);
        }
    }
}
