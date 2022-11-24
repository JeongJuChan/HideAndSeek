// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentRoomNameProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CurrentRoom.Name UI property.
    /// </summary>
    public class CurrentRoomNameProperty : PropertyListenerBase
    {
        public Text Text;

        string _cache = null;

        void Update()
        {

            if (PhotonNetwork.CurrentRoomListItem != null)
            {
                if ((PhotonNetwork.CurrentRoomListItem.Name != _cache))
                {
                    _cache = PhotonNetwork.CurrentRoomListItem.Name;
                    Text.text = _cache.ToString();
                    this.OnValueChanged();
                }
            }
            else
            {
                if (_cache == null)
                {
                    _cache = null;
                    Text.text = "n/a";
                }
            }
        }
    }
}