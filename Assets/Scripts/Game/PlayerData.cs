namespace com.tictactoe.game
{
	public class PlayerData
	{
		public enum PlayerType
		{
			Human = -1,
			Empty = 0,
			AI = 1
		}

		public TileData.Value tile { get; private set; }
		public PlayerType playerType { get; private set; }
		public bool isFirstPlayer => tile == TileData.Value.X;

		public void Reset(TileData.Value tile, PlayerType playerType)
		{
			this.tile = tile;
			this.playerType = playerType;
		}
	}
}
