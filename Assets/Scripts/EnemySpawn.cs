using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Gizmo Data")]
    public float directionLength = 1.0f;
    public Vector3 boxSize;


    public void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.enemySpawns.Add(this);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.enemySpawns.Remove(this);
        }
    }

    public void OnDrawGizmos()
    {
        Color spawnPointColor = Color.blue;
        spawnPointColor.a = 0.5f;
        Gizmos.color = spawnPointColor;

        float halfOfBoxHeight = boxSize.y / 2;
        Vector3 upByHalfOfBoxHeight = Vector3.up * halfOfBoxHeight;
        Gizmos.DrawCube(transform.position + upByHalfOfBoxHeight, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + upByHalfOfBoxHeight, transform.forward * directionLength);
    }
}
