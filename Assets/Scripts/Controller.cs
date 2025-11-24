using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;

    public virtual void Start()
    {
    }

    public virtual void Possess(Pawn pawnToPossess) 
    {
        pawn = pawnToPossess;
        pawnToPossess.controller = this;
    }

    public virtual void Unpossess()
    {
        pawn.controller = null;
        pawn = null;
    }

    public virtual void OnDestroy() 
    {
    }

}
