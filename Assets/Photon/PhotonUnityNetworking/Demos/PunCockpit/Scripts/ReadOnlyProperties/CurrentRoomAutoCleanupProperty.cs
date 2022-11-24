// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomAutoCleanupProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.AutoCleanUp UI property.
    /// </summary>
    public class CurrentRoomAutoCleanupProperty : PropertyListenerBase
    {

        public Text Text;

        int _cache = -1;

        void Update()
        {

            if (PhotonNetwork.CurrentRoomListItem != null && PhotonNetwork.CurrentRoomListItem.AutoCleanUp)
            {
                if ((PhotonNetwork.CurrentRoomListItem.AutoCleanUp && _cache != 1) || (!PhotonNetwork.CurrentRoomListItem.AutoCleanUp && _cache != 0))
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.AutoCleanUp ? 1 : 0;
                    Text.text = PhotonNetwork.CurrentRoomListItem.AutoCleanUp ? "true" : "false";
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