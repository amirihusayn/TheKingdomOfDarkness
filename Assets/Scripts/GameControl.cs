using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : singleton<GameControl>
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Text messageText;
    [SerializeField] private GameObject nextWaveButton;
    [SerializeField] private List<GameObject> pieceList;
    [SerializeField] private List<GameObject> deathParticlesList;
    bool isFinished = false;
    static System.Random random = new System.Random();
    private List<GameObject> grounds;

    public Color[] Colors
    {
        get
        {
            return colors;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        grounds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
    }

    private void CheckFinishState()
    {
        int availables = 0;
        string playerNumber = "";
        foreach (GameObject thisPiece in pieceList)
            if (thisPiece.activeSelf)
                availables++;
        if (availables == 1)
        {
            foreach (GameObject thisPiece in pieceList)
                if (thisPiece.activeSelf)
                    playerNumber = thisPiece.GetComponentInChildren<TextMesh>().text;
            messageText.text = playerNumber + " Won !";
            isFinished = true;
            nextWaveButton.SetActive(false);
        }
        if (availables == 0)
        {
            messageText.text = "Draw !";
            isFinished = true;
            nextWaveButton.SetActive(false);
        }
    }

    public void NextWave()
    {
        if (!isFinished)
        {
            int randomIndex;
            PieceElement thisPiece;
            for (int index = 0; index < 4; index++)
            {
                randomIndex = random.Next(0, grounds.Count);
                deathParticlesList[index].transform.position = grounds[randomIndex].transform.position;
                deathParticlesList[index].SetActive(true);
                deathParticlesList[index].GetComponent<ParticleSystem>().Play();
                thisPiece = grounds[randomIndex].GetComponent<GroundElement>().PieceElement;
                if (thisPiece != null)
                    thisPiece.OnDeath();
            }
        }
        CheckFinishState();
    }

    public void ReloadScene()
    {
        isFinished = false;
        nextWaveButton.SetActive(true);
        messageText.text = "Move and Survive !";
        foreach (GameObject thisPiece in pieceList)
        {
            thisPiece.transform.position = thisPiece.GetComponent<PieceElement>().InitialPosition;
            thisPiece.SetActive(true);
        }
    }
}
