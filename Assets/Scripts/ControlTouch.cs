using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlTouch : MonoBehaviour {
    // Fields
	private GameObject clickedElement, draggedElement;
    [SerializeField] private GameObject plane;
	[SerializeField] private LayerMask elementMask;
    Vector3 startPosition, endPosition, sideDirection;
    bool isClicked, isDraging, isClickReleased;

    // Properties
    public GameObject ClickedElement
    {
        get { return clickedElement; }
        private set {
            GroundElement groundElementComponent;
            // deselect privious ground element
            if(clickedElement != null)
            {
                groundElementComponent = clickedElement.GetComponent<GroundElement>();
                if(groundElementComponent != null)
                    groundElementComponent.OnDeselect();
            }
            clickedElement = value;
            // select current ground element
            if(clickedElement != null)
            {
                groundElementComponent = clickedElement.GetComponent<GroundElement>();
                if(groundElementComponent != null)
                    groundElementComponent.OnSelect();
            }
        }
    }

    public GameObject DraggedElement
    {
        get { return draggedElement; }
        private set {
            GroundElement groundElementComponent;
            // deselect privious dragged ground element
            if(draggedElement != null)
            {
                groundElementComponent = draggedElement.GetComponent<GroundElement>();
                if(groundElementComponent != null)
                    groundElementComponent.OnDragLeft();
            }
            draggedElement = value;
            // select current ground element
            if(draggedElement != null)
            {
                groundElementComponent = draggedElement.GetComponent<GroundElement>();
                if(groundElementComponent != null)
                    groundElementComponent.OnDrag();
            }
        }
    }

    // Methods
    void Start () {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
	}

	void Update () {
        isClicked = Input.GetKeyDown(KeyCode.Mouse0);
        isDraging = Input.GetKey(KeyCode.Mouse0);
        isClickReleased = Input.GetKeyUp(KeyCode.Mouse0);
	}

	void FixedUpdate() {
        if(isClicked)
            ClickElement();
        if(isDraging)
            DragElement();
        if(isClickReleased)
            MoveElement();
    }

    void ClickElement()
    {
        RaycastHit hit;
		Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, elementMask))
            if(hit.transform.GetComponent<GroundElement>() != null)
            {
                ClickedElement = hit.transform.gameObject;
                startPosition = hit.transform.position;
            }
            else if(hit.transform.GetComponentInParent<GroundElement>() != null)
            {
                ClickedElement = hit.transform.parent.gameObject;
                startPosition = ClickedElement.transform.position;
            }
            else
                startPosition = Vector3.zero;
    }

    void DragElement()
    {
        RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, elementMask))
            endPosition = hit.point;

        sideDirection = endPosition - startPosition;
        if(ClickedElement != plane && ClickedElement != null)
            if(Physics.Raycast(startPosition, sideDirection, out hit, 1.5f, elementMask))
                DraggedElement = hit.transform.gameObject;
        Debug.DrawRay(startPosition, sideDirection, Color.red);
    }

    void MoveElement()
    {
        GroundElement groundElementComponent;
        if(DraggedElement != null && ClickedElement != null)
        {
            groundElementComponent = ClickedElement.GetComponent<GroundElement>();
            if(groundElementComponent != null)
                groundElementComponent.OnMove(DraggedElement);
        }
        ClearElements();
    }

    void ClearElements()
    {
        ClickedElement = null;
        DraggedElement = null;
    }
}