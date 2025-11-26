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
        // If our controller is an AI controller, destroy it, too!
        Pawn pawn = GetComponent<Pawn>();
        ControllerAI controller = pawn.controller as ControllerAI; // <- try to convert (cast) to ControllerAI, if can't send null
        if (controller != null)
        {
            Destroy(controller.gameObject);
        }

        // Destroy ourselves
        Destroy(gameObject);
    }
}
