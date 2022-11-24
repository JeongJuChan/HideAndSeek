using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonController : MonoBehaviourPunCallbacks
{
    
    #region Unity Methods
    
    void Start()
    {
        string nickName = PlayerPrefs.GetString(Define.UserAuth.UserName.ToString());
        ConnectToPhoton(nickName);
    }

    void ConnectToPhoton(string nickName)
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    

    #endregion

    #region Private Methods

    

    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log($"OnConnectedToMaster");
    }

    #endregion
    
}
