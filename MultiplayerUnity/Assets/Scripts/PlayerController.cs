using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

        transform.parent = GameObject.Find("Canvas").transform;
    }
}
