using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    public RoomInfo Info { get; private set; }
    
    [SerializeField] TMP_Text _text;

    PhotonConnector _photonConnector;

    void Start()
    {
        _photonConnector = FindObjectOfType<PhotonConnector>();
    }

    public void SetRoomInfo(RoomInfo info)
    {
        Info = info;
        _text.text = $"{info.Name} ({info.PlayerCount}/{info.MaxPlayers})";
    }

    public void OnClickJoinRoomButton()
    {
        _photonConnector.JoinRoom(Info.Name);
    }
}
