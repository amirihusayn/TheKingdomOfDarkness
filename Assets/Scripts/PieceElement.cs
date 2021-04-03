using UnityEngine;

public class PieceElement : MonoBehaviour {
	private static int colorIndex = 0;
    private PiecePrototype pieceCore;
	[SerializeField] private Animator animator;

	private void Start() {
		int playerNumber = colorIndex + 1;
		pieceCore = new ManPiece();
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = GameControl.Instance.Colors[colorIndex];
		gameObject.GetComponentInChildren<TextMesh>().text = "P" + colorIndex;
		colorIndex++;
	}

	public void OnSelect()
	{
		// pieceCore.OnSelect(this);
	}

	public void OnDeselect()
	{
		// pieceCore.OnDeselect(this);
	}

	public void OnDrag()
	{
		animator.SetBool("isWalking", true);
		// pieceCore.OnDrag(this);	
	}

	public void OnDragLeft()
	{
		animator.SetBool("isWalking", false);
		// pieceCore.OnDragLeft(this);	
	}

	public void OnMove()
	{
		// animator.SetTrigger("OnJump");
		// pieceCore.OnMove(this);
	}

	public void OnDeath()
	{
		gameObject.SetActive(false);
	}
}