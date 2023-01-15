using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    [SerializeField] TMP_Text text;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

        transform.parent = GameObject.Find("Canvas").transform;
    }

    public void generate()
    {
        if (PV.IsMine)
        {
            PV.RPC("updateText", RpcTarget.AllBuffered, Random.Range(0, 1000).ToString());
        }
    }

    [PunRPC]
    void updateText(string number)
    {
        text.text = number;
    }
}

