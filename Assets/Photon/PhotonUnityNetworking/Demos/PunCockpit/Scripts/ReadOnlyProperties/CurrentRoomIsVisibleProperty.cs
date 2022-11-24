// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomIsVisibleProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.IsVisible UI property
    /// </summary>
    public class CurrentRoomIsVisibleProperty : PropertyListenerBase
    {

        public Text Text;

        int _cache = -1;

        void Update()
        {

            if (PhotonNetwork.CurrentRoomListItem != null)
            {
                if ((PhotonNetwork.CurrentRoomListItem.IsVisible && _cache != 1) || (!PhotonNetwork.CurrentRoomListItem.IsVisible && _cache != 0))
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.IsVisible ? 1 : 0;
                    Text.text = PhotonNetwork.CurrentRoomListItem.IsVisible ? "true" : "false";
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