using UnityEngine;

public class DeathDestroy : Death
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        // Destroy ourselves
        Destroy(gameObject);
    }
}
