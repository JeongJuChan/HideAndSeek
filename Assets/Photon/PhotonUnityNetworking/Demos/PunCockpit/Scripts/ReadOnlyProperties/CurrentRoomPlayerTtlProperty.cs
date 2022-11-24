// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomPlayerTtlProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.PlayerTtl UI property.
    /// </summary>
    public class CurrentRoomPlayerTtlProperty : PropertyListenerBase
    {
        public Text Text;

        int _cache = -1;

        void Update()
        {
            if (PhotonNetwork.CurrentRoomListItem != null)
            {
                if (PhotonNetwork.CurrentRoomListItem.PlayerTtl != _cache)
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.PlayerTtl;
                    Text.text = _cache.ToString();
                    this.OnValueChanged();
                }
            }
            else
            {
                if (_cache != -1)
                {
                    _cache = -1;
                    Text.text = "n/a";
                }
            }
        }
    }
}