using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundElement : MonoBehaviour
{
    // Fields
    [SerializeField] private PieceElement pieceElement;
    [SerializeField] private Animator animator;

    // Properties
    public PieceElement PieceElement
    {
        get { return pieceElement; }
        set { pieceElement = value; }
    }

    // Methods
    public void OnSelect()
    {
        animator.SetBool("isSelected", true);
        if (pieceElement != null)
            pieceElement.OnSelect();
    }

    public void OnDeselect()
    {
        animator.SetBool("isSelected", false);
        if (pieceElement != null)
            pieceElement.OnDeselect();
    }

    public void OnDrag()
    {
        animator.SetBool("isDrag", true);
        if (pieceElement != null)
            pieceElement.OnDrag();
    }

    public void OnDragLeft()
    {
        animator.SetBool("isDrag", false);
        if (pieceElement != null)
            pieceElement.OnDragLeft();
    }

    public void OnMove(GroundElement targetElement)
    {
        if (targetElement != null && pieceElement != null && targetElement.PieceElement == null)
        {
            pieceElement.transform.position = targetElement.transform.position;
            pieceElement.OnMove();
        }
    }
}
