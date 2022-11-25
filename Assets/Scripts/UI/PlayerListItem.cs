using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviour
{
    public Player PlayerItem { get; private set; }
    
    [SerializeField] TMP_Text _nameText;
    [SerializeField] GameObject _readyCheck;

    public bool IsActive { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        PlayerItem = player;
        _nameText.text = player.NickName;
    }

    public void SetCurrentReadyState()
    {
        Debug.Log("SetCurrentReadyState");
        _readyCheck.SetActive(IsActive);
    }


    public void SetReadyState(bool isActive)
    {
        Debug.Log($"SetReadyState : {isActive}");
        IsActive = isActive;
        _readyCheck.SetActive(IsActive);
    }
    
}
