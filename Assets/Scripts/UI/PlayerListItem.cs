using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public Player PlayerItem { get; private set; }
    
    [SerializeField] TMP_Text _nameText;
    [SerializeField] GameObject _readyCheck;
    [SerializeField] GameObject _masterIcon;
    [SerializeField] Image _image;

    public bool IsReadyActive { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        PlayerItem = player;
        _nameText.text = player.NickName;
    }

    public void SetCurrentReadyState()
    {
        Debug.Log("SetCurrentReadyState");
        _readyCheck.SetActive(IsReadyActive);
    }

    public void SetReadyState(bool isActive)
    {
        Debug.Log($"SetReadyState : {isActive}");
        IsReadyActive = isActive;
        _readyCheck.SetActive(IsReadyActive);
    }

    public void ActiveMasterIcon()
    {
        _masterIcon.SetActive(true);
    }

    public void SetLocalPlayerColor()
    {
        _image.color = Color.yellow;
    }
    
}
