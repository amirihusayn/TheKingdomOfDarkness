public abstract class PiecePrototype
{
    public abstract int NumberOfMoves { get; }
    public abstract int NumberOfLives { get; set; }
    public abstract void OnSelect(PieceElement piece);
    public abstract void OnDeselect(PieceElement piece);
    public abstract void OnDrag(PieceElement piece);
    public abstract void OnDragLeft(PieceElement piece);
    public abstract void OnMove(PieceElement piece);
}