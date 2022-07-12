using UnityEngine;

namespace com.tictactoe.game
{
	public class BoardRenderer : ScriptableObject, IBoardRenderer
	{
		public virtual void Render(BoardData board){}
		public virtual void RenderTileValue(int row, int col, TileData.Value val, BoardData board) {}
	}
}
