using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public void Possess(Pawn pawnToPossess) 
    {
        pawn = pawnToPossess;
        pawnToPossess.controller = this;
    }
        public void Unpossess()
    {
        pawn.controller = null;
        pawn = null;
    }

}
