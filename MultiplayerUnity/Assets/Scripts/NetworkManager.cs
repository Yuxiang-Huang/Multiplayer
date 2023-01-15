using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class NetworkManager: MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;

    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListPrefab;

    void Awake()
    {
        instance = this;
    }

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
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomName.text))
            return;

        PhotonNetwork.CreateRoom(roomName.text);
        ScreenManager.Instance.DisplayScreen("Loading");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + errorText;
        ScreenManager.Instance.DisplayScreen("Error");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        ScreenManager.Instance.DisplayScreen("Loading");
    }

    public override void OnJoinedRoom()
    {
        ScreenManager.Instance.DisplayScreen("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        //create players
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPrefab, playerListContent)
            .GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ScreenManager.Instance.DisplayScreen("Loading");
    }

    public override void OnLeftRoom()
    {
        ScreenManager.Instance.DisplayScreen("Main");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //clear list
        foreach (Transform transform in roomListContent)
        {
            Destroy(transform.gameObject);
        }

        //make room
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent)
            .GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
