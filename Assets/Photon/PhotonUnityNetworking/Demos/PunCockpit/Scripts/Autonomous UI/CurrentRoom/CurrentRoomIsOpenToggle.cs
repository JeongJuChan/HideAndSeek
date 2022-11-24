// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhotonNetwork.CurrentRoom.IsOpen.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit Demo
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
	/// <summary>
	/// PhotonNetwork.CurrentRoom.IsOpen UI Toggle
	/// </summary>
	[RequireComponent(typeof(Toggle))]
	public class CurrentRoomIsOpenToggle : MonoBehaviour, IPointerClickHandler
	{
		Toggle _toggle;


		// Use this for initialization
		void OnEnable()
		{
			_toggle = GetComponent<Toggle>();
		}

		void Update()
		{

			if (PhotonNetwork.CurrentRoomListItem == null && _toggle.interactable)
			{
				_toggle.interactable = false;
				
			}
			else if (PhotonNetwork.CurrentRoomListItem != null && !_toggle.interactable)
			{
				_toggle.interactable = true;
			}
			
			if (PhotonNetwork.CurrentRoomListItem!=null && PhotonNetwork.CurrentRoomListItem.IsOpen != _toggle.isOn)
			{
				Debug.Log("Update toggle : PhotonNetwork.CurrentRoom.IsOpen = " + PhotonNetwork.CurrentRoomListItem.IsOpen, this);
				_toggle.isOn = PhotonNetwork.CurrentRoomListItem.IsOpen;
			}
		}


		public void ToggleValue(bool value)
		{
			if (PhotonNetwork.CurrentRoomListItem != null)
			{
				Debug.Log("PhotonNetwork.CurrentRoom.IsOpen = " + value, this);
				PhotonNetwork.CurrentRoomListItem.IsOpen = value;
			}

			
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ToggleValue(_toggle.isOn);
		}
	}
}