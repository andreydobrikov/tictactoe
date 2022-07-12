using UnityEngine;

namespace com.tictactoe.game
{
	public interface IBoardRenderer
	{
		void Render(BoardData board);
		void RenderTileValue(int row, int col, TileData.Value val, BoardData board);
	}
}
