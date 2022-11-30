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
    // 테스트용 SerializeField    
    [SerializeField] int _readyCount = 0;
    
    LobbyUI _lobbyUI;
    List<RoomListItem> _roomList = new List<RoomListItem>();
    List<PlayerListItem> _playerList = new List<PlayerListItem>();
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
    
    public void ChangeReadyState()
    {
        Player player = PhotonNetwork.LocalPlayer;
        _hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
        
        int index = _playerList.FindIndex(p => p.PlayerItem.Equals(player));
        if (index != -1)
        {
            // 상태 변경이라 !Active로 한 것
            bool isActive = !_playerList[index].IsReadyActive;
            _hashtable[READY_KEY] = isActive;
            Debug.Log($"!_playerList[index].IsActive : {isActive}");
            PhotonNetwork.LocalPlayer.SetCustomProperties(_hashtable);
        }
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void CloseRoom()
    {
        _readyCount = 0;
        foreach (PlayerListItem playerListItem  in _playerList)
        {
            if (playerListItem.IsReadyActive)
            {
                _readyCount++;
            }
        }
        bool isAllReady = PhotonNetwork.CurrentRoomListItem.PlayerCount - 1 == _readyCount;
        if (PhotonNetwork.IsMasterClient && isAllReady)
        {
            PhotonNetwork.CurrentRoomListItem.IsVisible = false;
            PhotonNetwork.CurrentRoomListItem.IsOpen = false;
            Manager.Scene.LoadLevel(Define.SceneName.MainScene.ToString());
        }
    }

    public void IsLoggedIn()
    {
        
    }
    
    #endregion

    #region Private Methods
    
    void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (var roomInfo in roomList)
        {
            // 리스트에 존재하는지 확인
            int index = _roomList.FindIndex(r => r.Info.Name == roomInfo.Name);

            if (roomInfo.RemovedFromList)
            {
                if (_roomList.Count > 0)
                {
                    Debug.Log($"_roomList.Count : {_roomList.Count}");
                    Debug.Log($"index : {index}");
                    Debug.Log($"RemoveRoom");
                    Destroy(_roomList[index].gameObject);
                    _roomList.RemoveAt(index);
                }
            }
            else
            {
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
                else
                {
                    _roomList[index].SetRoomInfo(roomInfo);
                }
            }
        }
    }
    
    // 로컬 플레이어
    void UpdatePlayerList()
    {
        Debug.Log("UpdatePlayerList");
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Player player in players)
        {
            PlayerListItem playerListItem = _lobbyUI.CreateAndGetPlayerListItem();
            _playerList.Add(playerListItem);
            playerListItem.SetPlayerInfo(player);

            if (player.IsLocal)
            {
                playerListItem.SetLocalPlayerColor();
                if (PhotonNetwork.IsMasterClient)
                {
                    playerListItem.ActiveMasterIcon();
                }
            }
            else
            {
                _hashtable = player.CustomProperties;
                playerListItem.PlayerItem.SetCustomProperties(_hashtable);
                
                if (player.IsMasterClient)
                {
                    playerListItem.ActiveMasterIcon();
                }
            }
        }
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

        foreach (var playerListItem in _playerList)
        {
            if (playerListItem.PlayerItem.IsMasterClient)
                playerListItem.ActiveMasterIcon();
        }
    }
    
    void ClearRoomList()
    {
        foreach (PlayerListItem playerListItem in _playerList)
        {
            if (playerListItem)
            {
                Destroy(playerListItem.gameObject);
            }
        }
        _playerList.Clear();
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

    // 룸 리스트가 갱신이 되면 실행
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        _lobbyUI.MoveToRoom();
        _lobbyUI.UpdateRoomInfoUI(PhotonNetwork.CurrentRoomListItem);
        _lobbyUI.SettingMasterInRoom(PhotonNetwork.IsMasterClient);
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

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        foreach (PlayerListItem playerListItem in _playerList)
        {
            if (playerListItem.PlayerItem.Equals(newMasterClient))
            {
                _hashtable = newMasterClient.CustomProperties;
                _hashtable[READY_KEY] = false;
                playerListItem.PlayerItem.SetCustomProperties(_hashtable);
            }
        }

        _lobbyUI.SettingMasterInRoom(PhotonNetwork.IsMasterClient);
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
        bool isReadyActive = (bool)targetPlayer.CustomProperties[READY_KEY];
        //_readyCount += isReadyActive ? 1 : -1;
        _lobbyUI.OnReadyProperty(GetPlayerListItem(targetPlayer), isReadyActive);
        
    }
    #endregion


    
}
