// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomEmptyRoomTtlProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.EmptyRoomTtl UI property.
    /// </summary>
    public class CurrentRoomEmptyRoomTtlProperty : PropertyListenerBase
    {
        public Text Text;

        int _cache = -1;

        void Update()
        {
            if (PhotonNetwork.CurrentRoomListItem != null)
            {
                if (PhotonNetwork.CurrentRoomListItem.EmptyRoomTtl != _cache)
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.EmptyRoomTtl;
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