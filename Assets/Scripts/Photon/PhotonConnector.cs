using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    LobbyUI _lobbyUI;
    List<RoomListItem> _roomList = new List<RoomListItem>();
    List<RoomInfo> _roomInfos = new List<RoomInfo>();

    #region Unity Methods

    void Awake()
    {
        _lobbyUI = FindObjectOfType<LobbyUI>();
    }

    #endregion

    #region Public Methods

    public void ConnectToPhoton()
    {
        _lobbyUI.MoveToLobby();
        Debug.Log("ConnectToPhoton");
        string nickName = PlayerPrefs.GetString(Define.UserAuth.UserName.ToString());
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        Debug.Log($"CreateRoom {roomName}");
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { IsOpen = true, MaxPlayers = 8, IsVisible = true },
            TypedLobby.Default);
    }
    
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public string GetNickName()
    {
        return PhotonNetwork.NickName;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    #endregion

    #region Private Methods

    

    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        // 로딩창 텍스트 띄우기, 
        Debug.Log($"OnConnectedToMaster");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        _lobbyUI.OnLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"OnCreatedRoom");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomInfos = roomList;
        foreach (var roomInfo in roomList)
        {
            // 리스트에 존재하는지 확인
            int index = _roomList.FindIndex(r => r.Info.Name == roomInfo.Name);

            // 없으면 생성
            if (index == -1)
            {
                RoomListItem roomListItem = _lobbyUI.CreateRoomListUI();
                if (roomListItem)
                {
                    Debug.Log("CreateRoom");
                    roomListItem.SetRoomInfo(roomInfo);
                    _roomList.Add(roomListItem);
                }
            }
            // 존재하면 최신화
            else
            {
                if (roomInfo.RemovedFromList)
                {
                    Debug.Log("RemoveRoom");
                    Destroy(_roomList[index].gameObject);
                    _roomList.RemoveAt(index);
                }
                _roomList[index].SetRoomInfo(roomInfo);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
        Debug.Log("OnJoinedRoom");
        _lobbyUI.MoveToRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRoomFailed : {message}");
    }

    #endregion

    
}
