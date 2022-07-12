using UnityEngine;

namespace com.tictactoe.game
{
	[CreateAssetMenu(fileName = "DebugBoardRenderer", menuName = "Renderers/DebugBoardRenderer")]
	public class DebugBoardRenderer : BoardRenderer
	{
		public override void Render(BoardData board)
		{
			for (int row = 0; row < board.size; row++)
			{
				for (int col = 0; col < board.size; col++)
				{
					Debug.LogFormat("row: {0}, col: {1}, value: {2}", row, col, board.tileData[row, col].value);
				}
			}
		}

		public override void RenderTileValue(int row, int col, TileData.Value val, BoardData board)
		{
			Debug.LogFormat("row: {0}, col: {1}, value: {2}", row, col, val);
		}
	}
}
