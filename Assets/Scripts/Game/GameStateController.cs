using com.tictactoe.dispatcher;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.tictactoe.game
{
	public class GameStateController : MonoBehaviour
	{
		[SerializeField] private BoardController boardController;
		[SerializeField] private UIController uiController;
		[SerializeField] private SoundManager soundManager;

		private PlayerData[] players = new PlayerData[2];
		private int turnTaker = 0;

		private void Start()
		{
			GlobalEventDispatcher.Instance.dispatcher.AddListener<TileClickedEvent>(OnTileClicked);
		}

		private void OnDestroy()
		{
			GlobalEventDispatcher.Instance?.dispatcher.RemoveListener<TileClickedEvent>(OnTileClicked);
		}

		private void OnTileClicked(TileClickedEvent evt)
		{
			TileRenderer t = evt.model.tileRenderer;
			soundManager.PlayTileClickSound();
			StartCoroutine(SetTurn(t.tilePosition.row, t.tilePosition.col, players[turnTaker]));
		}

		public void ResetGame()
		{
			soundManager.PlayStartGameSound();
			uiController.ResetGame();
			turnTaker = 0;
			boardController.ResetBoard();
			players = new PlayerData[2];
			players[0] = new PlayerData();
			players[1] = new PlayerData();

			//randomly pick first player: 0-49 - human, 50-99 AI 
			int rand = Random.Range(0, 100);
			if (rand < 50)
			{
				players[0].Reset(TileData.Value.X, PlayerData.PlayerType.Human);
				players[1].Reset(TileData.Value.O, PlayerData.PlayerType.AI);
			}
			else
			{
				players[0].Reset(TileData.Value.X, PlayerData.PlayerType.AI);
				players[1].Reset(TileData.Value.O, PlayerData.PlayerType.Human);

				//start minmaxing
				TilePosition pos = MiniMaxEvaluator.FindBestMove(boardController.boardData, players[turnTaker].tile, Opponent().tile);
				StartCoroutine(SetTurn(pos.row, pos.col, players[turnTaker]));
			}

			var evtModel = new SetPlayerEventModel(players[turnTaker]);
			var evt = EventFactory.Instance.Create<SetPlayerEvent, SetPlayerEventModel>(evtModel);
			GlobalEventDispatcher.Instance.dispatcher.Dispatch(evt);
		}

		private IEnumerator SetTurn(int x, int y, PlayerData player)
		{
			//turn ai
			if (players[turnTaker].playerType == PlayerData.PlayerType.AI)
				yield return new WaitForSeconds(2f);

			boardController.SetTileValue(x, y, player.tile);

			//if we have a winner, just display it and go to start game again
			var winner = boardController.Winner;
			if (winner != TileData.Value.Empty)
			{
				if (player.playerType == PlayerData.PlayerType.Human)
				{
					soundManager.PlayHumanWinSound();
				}
				else
				{
					soundManager.PlayHumanLoseSound();
				}
				StartCoroutine(uiController.SetEndGame("Winner is: " + winner));
				yield break;
			}

			//if no more turn - bail
			if (boardController.boardData.IsBoardFull())
			{
				soundManager.PlayGameTieSound();
				StartCoroutine(uiController.SetEndGame("It's a tie!"));
				yield break;
			}

			//set the turn to the next player
			PassTurn();

			bool isAI = players[turnTaker].playerType == PlayerData.PlayerType.AI;

			//dispatch event to notify that the field should be interactable and notify ghost tiles
			var evtModel = new SetPlayerEventModel(players[turnTaker]);
			var evt = EventFactory.Instance.Create<SetPlayerEvent, SetPlayerEventModel>(evtModel);
			GlobalEventDispatcher.Instance.dispatcher.Dispatch(evt);

			//new turn taker
			if (isAI)
			{
				//start minmaxing
				TilePosition pos = MiniMaxEvaluator.FindBestMove(boardController.boardData, players[turnTaker].tile, Opponent().tile);
				StartCoroutine(SetTurn(pos.row, pos.col, players[turnTaker]));
			}
		}

		public PlayerData PassTurn()
		{
			turnTaker = turnTaker == 0 ? 1 : 0;
			return players[turnTaker];
		}

		public PlayerData Opponent()
		{
			int opponent = turnTaker == 0 ? 1 : 0;
			return players[opponent];
		}
	}
}
