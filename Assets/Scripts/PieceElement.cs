using System.Collections;
using UnityEngine;

public class PieceElement : MonoBehaviour
{
    // Fields
    private static int colorIndex = 0;
    private PiecePrototype pieceCore;
    private Vector3 initialPosition;
    [SerializeField, Range(-180, 180)] private float poseAngle;
    [SerializeField] private GroundElement loacationElement;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private TextMesh playerNumberText;
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
        Initialization();
        StopCoroutine("SoftRotate");
        StartCoroutine("SoftRotate");
    }

    private void Initialization()
    {
        int playerNumber = colorIndex + 1;
        pieceCore = new ManPiece();
        initialPosition = transform.position;
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = GameControl.Instance.Colors[colorIndex];
        playerNumberText.text = "P" + playerNumber;
        colorIndex++;
    }

    private void OnTriggerEnter(Collider other)
    {
        GroundElement enterdLocation = other.GetComponent<GroundElement>();
        if (enterdLocation == null)
            return;
        enterdLocation.PieceElement = this;
        LoacationElement = enterdLocation;
    }

    private void OnTriggerExit(Collider other)
    {
        GroundElement exitedLocation = other.GetComponent<GroundElement>();
        if (exitedLocation == null)
            return;
        exitedLocation.PieceElement = null;
        LoacationElement = null;
    }

    public void RotatePiece(Vector3 endDragPosition)
    {
        float angle;
        Vector3 direction;
        direction = endDragPosition - transform.position;
        angle = Quaternion.LookRotation(direction).eulerAngles.y;
        rigidBody.MoveRotation(Quaternion.Euler(0, angle, 0));
        playerNumberText.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerNumberText.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void RotatePiece(float angle)
    {
        rigidBody.MoveRotation(Quaternion.Euler(0, angle, 0));
        playerNumberText.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator SoftRotate()
    {
        float currentAngle;
        while(rigidBody.rotation.eulerAngles.y != poseAngle)
        {
            currentAngle = Vector3.Lerp(rigidBody.rotation.eulerAngles, new Vector3(0, poseAngle, 0), 0.1f).y;
            RotatePiece(currentAngle);
            yield return null;
        }
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
        StopCoroutine("SoftRotate");
        StartCoroutine("SoftRotate");
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        loacationElement.PieceElement = null;
    }
}