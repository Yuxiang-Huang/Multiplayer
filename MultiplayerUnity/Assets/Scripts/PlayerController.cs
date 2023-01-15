using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    public Button revealBtn;
    public Button winCard;
    public Button loseCard;

    List<string> allPhrases = new List<string>();

    //need to sync
    public int cardNum;
    public string phrase;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI displayCard;
    public TextMeshProUGUI displayPhrase;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

        transform.SetParent(GameObject.Find("Canvas").transform);

        transform.localScale = new Vector3(1, 1, 1);

        cardNum = 5;

        createList();
        generatePhrase();
    }

    void createList()
    {
        //look
        allPhrases.Add("Look Up");
        allPhrases.Add("Look Down");
        allPhrases.Add("Look Back");

        //do
        allPhrases.Add("Hesitate");
        allPhrases.Add("Compare");
        allPhrases.Add("Ask");
        allPhrases.Add("Laugh");
        allPhrases.Add("Stand");

        allPhrases.Add("Help");
        allPhrases.Add("Point using finger");
        allPhrases.Add("Recall");

        //say
        allPhrases.Add("Agree");
        allPhrases.Add("Disagree");

        allPhrases.Add("Reject to answer");

        allPhrases.Add("Repeat");

        allPhrases.Add("Guess the phrase");
        allPhrases.Add("Praise");
        allPhrases.Add("Touch head");

        //speak
        allPhrases.Add("Talk about time");

        allPhrases.Add("Say any name");
        allPhrases.Add("Say any food");
        allPhrases.Add("Say any subject");
        allPhrases.Add("Say any animal");

        allPhrases.Add("Say 'If'");
        allPhrases.Add("Say 'No'");
        allPhrases.Add("Say 'Unbelievable'");
    }

    public void reveal()
    {
        if (!PV.IsMine) return;

        winCard.gameObject.SetActive(true);
        loseCard.gameObject.SetActive(true);
        displayPhrase.gameObject.SetActive(true);
        revealBtn.gameObject.SetActive(false);
    }

    public void win()
    {
        if (!PV.IsMine) return;

        cardNum++;

        generatePhrase();
    }

    public void lose()
    {
        if (!PV.IsMine) return;

        cardNum--;

        generatePhrase();
    }

    public void generatePhrase()
    {
        if (!PV.IsMine) return;

        phrase = allPhrases[Random.Range(0, allPhrases.Count)];

        PV.RPC("updatePhrase", RpcTarget.AllBuffered, PhotonNetwork.NickName, cardNum, phrase);
    }

    [PunRPC]
    void updatePhrase(string name, int number, string phrase)
    {
        playerName.text = name;
        displayCard.text = "Cards Left: " + number;
        displayPhrase.text = phrase;

        winCard.gameObject.SetActive(false);
        loseCard.gameObject.SetActive(false);

        if (PV.IsMine)
        {
            revealBtn.gameObject.SetActive(true);
            displayPhrase.gameObject.SetActive(false);
        }
        else
        {
            revealBtn.gameObject.SetActive(false);
            displayPhrase.gameObject.SetActive(true);
        }
    }
}

