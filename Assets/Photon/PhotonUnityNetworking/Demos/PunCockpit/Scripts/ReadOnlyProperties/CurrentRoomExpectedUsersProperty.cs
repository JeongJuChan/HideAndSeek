// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomExpectedUsersProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.ExpectedUsers UI property.
    /// </summary>
    public class CurrentRoomExpectedUsersProperty : PropertyListenerBase
    {
        public Text Text;

        string[] _cache = null;

        void Update()
        {

            if (PhotonNetwork.CurrentRoomListItem == null || PhotonNetwork.CurrentRoomListItem.ExpectedUsers == null)
            {
                if (_cache != null)
                {
                    _cache = null;
                    Text.text = "n/a";
                }

                return;

            }

            if (_cache == null || (PhotonNetwork.CurrentRoomListItem.ExpectedUsers != null && !PhotonNetwork.CurrentRoomListItem.ExpectedUsers.SequenceEqual(_cache)))
            {

                Text.text = string.Join("\n", PhotonNetwork.CurrentRoomListItem.ExpectedUsers);

                this.OnValueChanged();

                return;
            }

            if (PhotonNetwork.CurrentRoomListItem.ExpectedUsers == null && _cache != null)
            {

                Text.text = string.Join("\n", PhotonNetwork.CurrentRoomListItem.ExpectedUsers);

                this.OnValueChanged();

                return;
            }
        }
    }
}