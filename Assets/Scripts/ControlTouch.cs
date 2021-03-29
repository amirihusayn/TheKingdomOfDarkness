using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlTouch : MonoBehaviour {
	private GameObject clickedElement, draggedElement;
    [SerializeField] private GameObject plane;
	[SerializeField] private LayerMask elementMask;
    Vector3 startPosition, endPosition, sideDirection;

    private GameObject DraggedElement
    {
        get { return draggedElement; }
        set {
            // reset privious dragged element or set animation trigger to idle
            if(draggedElement != null)
                draggedElement.GetComponent<MeshRenderer>().material.color = Color.white;
            draggedElement = value;
            // colorize it or set animation trigger
            if(draggedElement != null)
                draggedElement.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private GameObject ClickedElement
    {
        get { return clickedElement; }
        set {
            // reset privious dragged element or set animation trigger to idle
            if(clickedElement != null)
                clickedElement.GetComponent<MeshRenderer>().material.color = Color.white;
            clickedElement = value;
            // colorize it or set animation trigger
            if(clickedElement != null)
                clickedElement.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    // Use this for initialization
    void Start () {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            TouchElement();

        if(Input.GetKey(KeyCode.Mouse0))
            DragElement();
        
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            ClickedElement = null;
            if(draggedElement != null)
                draggedElement.GetComponent<MeshRenderer>().material.color = Color.white;
            draggedElement = null;
        }
    }

    private void TouchElement()
    {
        RaycastHit hit;
		Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, elementMask))
        {
            startPosition = hit.transform.position;
            ClickedElement = hit.transform.gameObject;
        }
    }

    private void DragElement()
    {
        RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, elementMask))
            endPosition = hit.point;

        sideDirection = endPosition - startPosition;
        if(clickedElement != plane)
            if(Physics.Raycast(startPosition, sideDirection, out hit, sideDirection.magnitude, elementMask))
                DraggedElement = hit.transform.gameObject;
        Debug.DrawRay(startPosition, sideDirection, Color.red);
    }
}