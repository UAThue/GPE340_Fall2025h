using UnityEngine;

public class DeathEnemy : MonoBehaviour
{
    public void Die()
    {
        // Destroy the controller, too
        Pawn pawn = GetComponent<Pawn>();
        Destroy(pawn.controller);
    }

}
