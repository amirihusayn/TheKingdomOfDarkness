using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlTouch : singleton<ControlTouch>
{
    // Fields
    private GroundElement clickedElement, draggedElement;
    [SerializeField] private GameObject plane;
    [SerializeField] private LayerMask elementMask;
    private Vector3 startPosition, endPosition, sideDirection;
    private bool isClicked, isDraging, isClickReleased;

    // Properties
    public GroundElement ClickedElement
    {
        get { return clickedElement; }
        private set
        {
            if (clickedElement != null)
                clickedElement.OnDeselect();
            clickedElement = value;
            if (clickedElement != null)
                clickedElement.OnSelect();
        }
    }

    public GroundElement DraggedElement
    {
        get { return draggedElement; }
        private set
        {
            if (draggedElement != null)
                draggedElement.OnDragLeft();
            draggedElement = value;
            if (draggedElement != null)
                draggedElement.OnDrag();
        }
    }

    // Methods
    protected override void Awake()
    {
        base.Awake();
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
    }

    private void Update()
    {
        isClicked = Input.GetKeyDown(KeyCode.Mouse0);
        isDraging = Input.GetKey(KeyCode.Mouse0);
        isClickReleased = Input.GetKeyUp(KeyCode.Mouse0);
    }

    private void FixedUpdate()
    {
        if (isClicked)
            ClickElement();
        if (isDraging)
        {
            DragElement();
            RotatePiece();
        }
        if (isClickReleased)
            MoveElement();
    }

    private void ClickElement()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, elementMask))
            if (hit.transform.GetComponent<GroundElement>() != null)
            {
                ClickedElement = hit.transform.GetComponent<GroundElement>();
                startPosition = hit.transform.position;
            }
            else if (hit.transform.GetComponent<PieceElement>() != null)
            {
                ClickedElement = hit.transform.GetComponent<PieceElement>().LoacationElement;
                startPosition = ClickedElement.transform.position;
            }
            else
                startPosition = Vector3.zero;
    }

    private void DragElement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, elementMask))
            if (hit.transform.GetComponent<GroundElement>() != null)
                endPosition = hit.transform.position;
            else if (hit.transform.GetComponent<PieceElement>() != null)
                endPosition = hit.transform.GetComponent<PieceElement>().LoacationElement.transform.position;
            else
                endPosition = hit.point;

        sideDirection = endPosition - startPosition;
        if (ClickedElement != null)
            if (Physics.Raycast(startPosition, sideDirection, out hit, 2f, elementMask))
                if (hit.transform.GetComponent<GroundElement>() != null)
                    DraggedElement = hit.transform.GetComponent<GroundElement>();
        Debug.DrawRay(startPosition, sideDirection, Color.red);
    }

    private void RotatePiece()
    {
        float angle;
        Rigidbody pieceRigidBody;
        Vector3 direction;
        PieceElement piece = null;
        if(ClickedElement != null)
            piece = ClickedElement.PieceElement;
        if(piece != null)
        {
            pieceRigidBody = piece.GetComponent<Rigidbody>();
            direction = endPosition - piece.transform.position;
            angle = Quaternion.LookRotation(direction).eulerAngles.y;
            pieceRigidBody.MoveRotation(Quaternion.Euler(0, angle, 0));
        }
    }

    private void MoveElement()
    {
        if (DraggedElement != null && ClickedElement != null)
            ClickedElement.OnMove(DraggedElement);
        ClickedElement.OnDeselect();
        DraggedElement.OnDragLeft(); 
    }
}