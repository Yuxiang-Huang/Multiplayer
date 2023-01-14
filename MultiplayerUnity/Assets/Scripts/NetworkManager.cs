using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager: MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_Text errorText;

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

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomName.text))
            return;

        PhotonNetwork.CreateRoom(roomName.text);
        ScreenManager.Instance.DisplayScreen("Main");
    }

    public override void OnJoinedRoom()
    {
        ScreenManager.Instance.DisplayScreen("Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + errorText;
        ScreenManager.Instance.DisplayScreen("Error");
    }
}
