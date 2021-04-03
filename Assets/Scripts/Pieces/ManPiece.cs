using UnityEngine;

public class ManPiece : PiecePrototype
{
    public override int NumberOfMoves { get {return 1;} }
    public override int NumberOfLives { get {return 1;} set {} }


    public override void OnSelect(PieceElement piece)
    {
    }

    public override void OnDeselect(PieceElement piece)
    {
    }

    public override void OnDrag(PieceElement piece)
    {
    }

    public override void OnDragLeft(PieceElement piece)
    {
    }

    public override void OnMove(PieceElement piece)
    {
    }
}