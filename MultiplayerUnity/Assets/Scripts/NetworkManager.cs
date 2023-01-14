using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager: MonoBehaviourPunCallbacks
{
    public void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        ScreenManager.Instance.DisplayScreen("Main");
        Debug.Log("Joined Lobby");
    }

    public void Update()
    {
        
    }
}
