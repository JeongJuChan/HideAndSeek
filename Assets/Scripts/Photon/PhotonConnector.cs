using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    LobbyUI _lobbyUI;
    List<RoomListItem> _roomList = new List<RoomListItem>();
    // 테스트용 SerializeField    
    [SerializeField] List<PlayerListItem> _playerList = new List<PlayerListItem>();
    Hashtable _hashtable;
    const string READY_KEY = "Ready"; 

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
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "room";
        }
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

    public void SetPlayerReady(bool isActive)
    {
        _hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
        // _hashtable.Add(READY_KEY, true);
        _hashtable[READY_KEY] = isActive;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_hashtable);
    }
    
    public void ChangeReadyState()
    {
        Player player = PhotonNetwork.LocalPlayer;
        _hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
        
        int index = _playerList.FindIndex(p => p.PlayerItem.Equals(player));
        if (index != -1)
        {
            _hashtable[READY_KEY] = !_playerList[index].IsActive;
            Debug.Log($"!_playerList[index].IsActive : {!_playerList[index].IsActive}");
            PhotonNetwork.LocalPlayer.SetCustomProperties(_hashtable);
        }
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Private Methods
    // 자신이 들어왔을 때
    
    
    void UpdateRoomList(List<RoomInfo> roomList)
    {
        // _roomInfos = roomList;
        foreach (var roomInfo in roomList)
        {
            // 리스트에 존재하는지 확인
            int index = _roomList.FindIndex(r => r.Info.Name == roomInfo.Name);

            // 없으면 생성
            if (index == -1)
            {
                RoomListItem roomListItem = _lobbyUI.CreateAndGetRoomListItem();
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
    
    // 로컬
    void UpdatePlayerList()
    {
        Debug.Log("UpdatePlayerList");
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Player player in players)
        {
            PlayerListItem playerListItem = _lobbyUI.CreateAndGetPlayerListItem();
            _playerList.Add(playerListItem);
            playerListItem.SetPlayerInfo(player);
            
            if (player.Equals(PhotonNetwork.LocalPlayer))
            {
                SetPlayerReady(false);
            }
            else
            {
                _hashtable = player.CustomProperties;
                playerListItem.PlayerItem.SetCustomProperties(_hashtable);
            }
        }
    }

    void ClearRoomList()
    {
        foreach (PlayerListItem playerListItem in _playerList)
        {
            Destroy(playerListItem.gameObject);
        }
        _playerList.Clear();
    }
    
    // 리모트 플레이어
    // PlayerList는 아직 최신화하지 않았으니 같은 게 있다면 삭제할 플레이어이므로 삭제 후 리스트에서 제거
    // 없다면 새 플레이어이므로 생성 후 리스트에 추가
    void UpdatePlayerList(Player otherPlayer)
    {
        Debug.Log("UpdatePlayerList(Player otherPlayer)");
        Debug.Log(_playerList.Count);
        int index = _playerList.FindIndex(p => p.PlayerItem.Equals(otherPlayer));
        if (index == -1)
        {
            PlayerListItem playerListItem = _lobbyUI.CreateAndGetPlayerListItem();
            _playerList.Add(playerListItem);
            playerListItem.SetPlayerInfo(otherPlayer);
            playerListItem.SetCurrentReadyState();
        }
        else
        {
            Debug.Log("Destroy Playerlist Player");
            Destroy(_playerList[index].gameObject);
            _playerList.RemoveAt(index);
        }
    }
    
    PlayerListItem GetPlayerListItem(Player player)
    {
        foreach (PlayerListItem playerListItem in _playerList)
        {
            if (playerListItem.PlayerItem.Equals(player))
            {
                return playerListItem;
            }
        }
        return null;
    }

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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"OnCreateRoomFailed : {message}");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        _lobbyUI.MoveToRoom();
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
        UpdatePlayerList();
    }

    public override void OnLeftRoom()
    {
        ClearRoomList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
        UpdatePlayerList(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
        UpdatePlayerList(otherPlayer);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRoomFailed : {message}");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        // 해시에 레디 키 값 없으면 탈출
        if (!changedProps.ContainsKey(READY_KEY))
            return;
        _lobbyUI.OnReadyProperty(GetPlayerListItem(targetPlayer), (bool)targetPlayer.CustomProperties[READY_KEY]);
    }
    #endregion


    
}
