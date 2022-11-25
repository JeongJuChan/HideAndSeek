using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] GameObject _LoginPanel;
    [SerializeField] GameObject _LobbyPanel;
    [SerializeField] GameObject _lodingText;
    [SerializeField] GameObject _RoomPanel;
    [SerializeField] RoomListItem _roomListItem;
    [SerializeField] PlayerListItem _playerListItem;
    [SerializeField] Transform _roomContent;
    [SerializeField] Transform _playerListUI;
    [SerializeField] TMP_InputField _roomInputField;
    [SerializeField] TMP_Text _nickNameText;
    [SerializeField] TMP_Text _roomInfoText;

    PhotonConnector _photonConnector;
    string _roomName;

    #region Unity Methods

    void Start()
    {
        _photonConnector = FindObjectOfType<PhotonConnector>();
        _LoginPanel.SetActive(true);
    }
    
    #endregion

    #region Public Methods

    public void MoveToLobby()
    {
        _LoginPanel.SetActive(false);
        _lodingText.SetActive(true);
    }

    public void OnLobby()
    {
        _lodingText.SetActive(false);
        _LobbyPanel.SetActive(true);
        _nickNameText.text = _photonConnector.GetNickName();
    }

    public void SetRoomName(string roomName)
    {
        _roomName = roomName;
    }

    public void OnClickCreateRoomButton()
    {
        _photonConnector.CreateRoom(_roomName);
    }

    public void MoveToRoom()
    {
        _LobbyPanel.SetActive(false);
        _RoomPanel.SetActive(true);
    }

    public void OnBackToLoginButton()
    {
        _LobbyPanel.SetActive(false);
        _LoginPanel.SetActive(true);
        _photonConnector.Disconnect();
    }

    public void OnClickReadyButton()
    {
        Debug.Log("OnClickReadyButton");
        _photonConnector.ChangeReadyState();
    }

    public RoomListItem CreateAndGetRoomListItem()
    {
        RoomListItem roomListItem = Instantiate(_roomListItem, _roomContent);
        return roomListItem;
    }
    
    public void UpdateRoomInfoUI(Photon.Realtime.RoomListItem item)
    {
        Debug.Log($"SetRoomInfo : {item.Name}\n({item.PlayerCount}/{item.MaxPlayers}");
        _roomInfoText.text = $"{item.Name}\n({item.PlayerCount}/{item.MaxPlayers})";
    }

    public PlayerListItem CreateAndGetPlayerListItem()
    {
        PlayerListItem playerListItem = Instantiate(_playerListItem, _playerListUI);
        return playerListItem;
    }

    public void OnReadyProperty(PlayerListItem playerListItem, bool isActive)
    {
        playerListItem.SetReadyState(isActive);
    }

    public void OnClickExitButton()
    {
        _RoomPanel.SetActive(false);
        _photonConnector.LeaveRoom();
    }

    #endregion

    #region Private Methods

    

    #endregion


    
}
