using System;
using UnityEngine;

namespace com.tictactoe.game
{
	public class BoardController : MonoBehaviour
	{
		[SerializeField] private BoardRenderer boardRenderer;
		[SerializeField] private BoardConfig boardConfig;

		public BoardData boardData { get; set; }

		private void Awake()
		{
			boardData = new BoardData();
		}

		public void ResetBoard()
		{
			boardData.ResetBoard(boardConfig);
			boardRenderer.Render(boardData);
		}

		public void SetBoard(BoardData board)
		{
			boardData = board;
			boardRenderer.Render(board);
		}

		public void SetTileValue(int row, int col, TileData.Value val)
		{
			boardData = boardData.SetTileValue(row, col, val);
			boardRenderer.RenderTileValue(row, col, val, boardData);
		}

		public TileData.Value GetTileValue(int x, int y)
		{
			return boardData.GetTileValue(x, y);
		}

		public TileData.Value Winner => boardData.Winner();
	}
}
