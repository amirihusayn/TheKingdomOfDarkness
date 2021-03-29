using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElement : MonoBehaviour {
	[SerializeField] private LayerMask elementMask;
	[SerializeField] private float detectorRayLength;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		// SideElement();
	}

	GameObject SideElement() {
		RaycastHit hit;
		Vector3 target = transform.position;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, elementMask))
			target = hit.transform.position;
		Vector3 direction = target - transform.position;
		Debug.DrawRay(transform.position, direction, Color.green);
		if(Physics.Raycast(transform.position, direction, out hit, detectorRayLength, elementMask, QueryTriggerInteraction.UseGlobal))
			return hit.transform.gameObject;
		else 
			return null;
	}
}
