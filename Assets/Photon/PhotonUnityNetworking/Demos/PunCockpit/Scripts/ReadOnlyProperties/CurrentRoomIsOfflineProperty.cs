// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomIsOfflineProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.IsOffline UI property
    /// </summary>
	public class CurrentRoomIsOfflineProperty : PropertyListenerBase
    {

        public Text Text;

        int _cache = -1;

        void Update()
        {

            if (PhotonNetwork.CurrentRoomListItem != null)
            {
				if ((PhotonNetwork.CurrentRoomListItem.IsOffline && _cache != 1) || (!PhotonNetwork.CurrentRoomListItem.IsOffline && _cache != 0))
                {
					_cache = PhotonNetwork.CurrentRoomListItem.IsOffline ? 1 : 0;
					Text.text = PhotonNetwork.CurrentRoomListItem.IsOffline ? "true" : "false";
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