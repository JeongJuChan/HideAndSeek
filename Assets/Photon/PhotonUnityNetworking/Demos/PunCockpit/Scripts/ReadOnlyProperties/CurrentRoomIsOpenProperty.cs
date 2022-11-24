// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomIsOpenProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.IsOpen UI property.
    /// </summary>
    public class CurrentRoomIsOpenProperty : PropertyListenerBase
    {
        public Text Text;

        int _cache = -1;

        void Update()
        {
            if (PhotonNetwork.CurrentRoomListItem != null)
            {
                if ((PhotonNetwork.CurrentRoomListItem.IsOpen && _cache != 1) || (!PhotonNetwork.CurrentRoomListItem.IsOpen && _cache != 0))
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.IsOpen ? 1 : 0;
                    Text.text = PhotonNetwork.CurrentRoomListItem.IsOpen ? "true" : "false";
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