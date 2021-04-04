using UnityEngine;

public class PieceElement : MonoBehaviour
{
    // Fields
    private static int colorIndex = 0;
    private PiecePrototype pieceCore;
    private Vector3 initialPosition;
    [SerializeField]private GroundElement loacationElement;
    [SerializeField] private Animator animator;

    // Properties
    public Vector3 InitialPosition { get { return initialPosition; } }

    public GroundElement LoacationElement
    {
        get { return loacationElement; }
        private set { loacationElement = value; }
    }

    private void Start()
    {
        SetColorAndPlayerNumber();
        initialPosition = transform.position;
    }

    private void SetColorAndPlayerNumber()
    {
        int playerNumber = colorIndex + 1;
        pieceCore = new ManPiece();
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = GameControl.Instance.Colors[colorIndex];
        gameObject.GetComponentInChildren<TextMesh>().text = "P" + colorIndex;
        colorIndex++;
    }

    void OnTriggerEnter(Collider other)
    {
        GroundElement enterdLocation = other.GetComponent<GroundElement>();
        if (enterdLocation == null)
            return;
        enterdLocation.PieceElement = this;
        LoacationElement = enterdLocation;
    }

    void OnTriggerExit(Collider other)
    {
        GroundElement exitedLocation = other.GetComponent<GroundElement>();
        if (exitedLocation == null)
            return;
        exitedLocation.PieceElement = null;
        LoacationElement = null;
    }

    public void OnSelect()
    {
        animator.SetBool("isWalking", true);
        // pieceCore.OnSelect(this);
    }

    public void OnDeselect()
    {
        animator.SetBool("isWalking", false);
        // pieceCore.OnDeselect(this);
    }

    public void OnDrag()
    {
        // pieceCore.OnDrag(this);	
    }

    public void OnDragLeft()
    {
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
        loacationElement.PieceElement = null;
    }
}