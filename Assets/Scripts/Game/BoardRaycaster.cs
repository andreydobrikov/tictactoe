using System;
using com.tictactoe.dispatcher;
using UnityEngine;
using UnityEngine.UI;

namespace com.tictactoe.game
{
	public class BoardRaycaster : MonoBehaviour
	{
		[SerializeField] private GraphicRaycaster graphicRaycaster;

		private void Awake()
		{
			GlobalEventDispatcher.Instance.dispatcher.AddListener<SetPlayerEvent>(OnSetPlayer);
		}

		private void OnDestroy()
		{
			GlobalEventDispatcher.Instance?.dispatcher.RemoveListener<SetPlayerEvent>(OnSetPlayer);
		}

		private void OnSetPlayer(SetPlayerEvent evt)
		{
			graphicRaycaster.enabled = evt.model.player.playerType == PlayerData.PlayerType.Human;
		}
	}
}
