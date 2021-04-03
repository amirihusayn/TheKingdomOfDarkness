using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundElement : MonoBehaviour {
	[SerializeField] private PieceElement pieceElement;
	[SerializeField] private Animator animator;

    void Start() {
		pieceElement = GetComponentInChildren<PieceElement>();
	}

    public void OnSelect()
	{
		animator.SetBool("isSelected", true);
		if(pieceElement != null)
			pieceElement.OnSelect();
	}

	public void OnDeselect()
	{
		animator.SetBool("isSelected", false);
		if(pieceElement != null)
			pieceElement.OnDeselect();
	}

	public void OnDrag()
	{
		animator.SetBool("isDrag", true);
		if(pieceElement != null)
			pieceElement.OnDrag();
	}

	public void OnDragLeft()
	{
		animator.SetBool("isDrag", false);
		if(pieceElement != null)
			pieceElement.OnDragLeft();
	}

	public void OnMove(GameObject targetElement)
	{
		GameObject pieceGameObject = null;
		GroundElement targetGroundElement = targetElement.GetComponent<GroundElement>();
		pieceElement = GetComponentInChildren<PieceElement>();
		if(pieceElement != null)
			pieceGameObject = pieceElement.gameObject;
		if(targetGroundElement != null && pieceGameObject != null && targetGroundElement.GetComponentInChildren<PieceElement>() == null)
		{
			pieceGameObject.transform.SetParent(targetElement.transform);
			pieceGameObject.transform.localPosition = new Vector3(0, 1, 0);
			pieceGameObject.transform.localScale = new Vector3(0.5f, 2.5f, 0.5f);
			pieceElement.OnMove();
		}
	}
}
