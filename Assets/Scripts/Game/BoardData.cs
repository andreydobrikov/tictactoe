namespace com.tictactoe.game
{
	public class BoardData
	{
		private TileData[,] gameboard;
		private int playerTiles = 0;

		public TileData[,] tileData => gameboard;	//public getter for minmaxing
		public int size = 3;

		public TileData[,] ResetBoard(BoardConfig boardConfig)
		{
			gameboard = new TileData[size, size];
			playerTiles = 0;

			for (int row = 0; row < size; row++)
			{
				for (int col = 0; col < size; col++)
				{
					gameboard[row, col] = new TileData();

					if (boardConfig != null)
					{
						gameboard[row, col].value = boardConfig.rows[row].tiles[col].value;
					}
				}
			}

			//test board
			/*
			var O = TileData.Value.O;
			var X = TileData.Value.X;
			gameboard[0, 0].value = O;
			gameboard[0, 2].value = X;
			gameboard[0, 1].value = X;
			gameboard[1, 0].value = X;
			gameboard[1, 1].value = O;
			gameboard[2, 0].value = O;
			*/

			return gameboard;
		}

		public BoardData SetTileValue(int row, int col, TileData.Value val)
		{
			if (gameboard[row, col].value == val)
				return this;

			gameboard[row, col].value = val;
			if (val == TileData.Value.Empty)
			{
				playerTiles--;
			}
			else
			{
				playerTiles++;
			}
			return this;
		}

		public TileData.Value GetTileValue(int row, int col)
		{
			return gameboard[row, col].value;
		}

		// This function returns true if there are no moves
		// remaining on the board. It returns false if
		// there are moves left to play.
		public bool IsBoardFull()
		{
			return (playerTiles == size * size);
		}

		public TileData.Value Winner()
		{
			// Checking for rows 
			bool bailOut = false;
			for (int row = 0; row < size; row++)
			{
				bailOut = false;
				for (int col = 0; col < size - 1; col++)
				{
					if (gameboard[row, col].value != gameboard[row, col + 1].value ||
						gameboard[row, col].value == TileData.Value.Empty)
					{
						bailOut = true;
						break;
					}
				}

				if (!bailOut)
					return gameboard[row, 0].value;
			}

			// Checking for columns
			for (int col = 0; col < size; col++)
			{
				bailOut = false;
				for (int row = 0; row < size - 1; row++)
				{
					if (gameboard[row, col].value != gameboard[row + 1, col].value ||
						(gameboard[row, col].value == TileData.Value.Empty))
					{
						bailOut = true;
						break;
					}
				}

				if (!bailOut)
					return gameboard[0, col].value;
			}

			bailOut = false;
			for (int col = 0; col < size - 1; col++)
			{
				// Checking for diagonals for X or O victory.
				if (gameboard[col, col].value != gameboard[col + 1, col + 1].value ||
					(gameboard[col, col].value == TileData.Value.Empty))
				{
					bailOut = true;
					break;
				}
			}

			if (!bailOut)
				return gameboard[0, 0].value;

			bailOut = false;
			for (int col = size - 1; col > 0; col--)
			{
				if (gameboard[size - 1 - col, col].value != gameboard[size - col, col - 1].value ||
					(gameboard[size - 1 - col, col].value == TileData.Value.Empty))
				{
					bailOut = true;
					break;
				}

			}

			if (!bailOut)
				return gameboard[0, size - 1].value;

			return TileData.Value.Empty;
		}

		private bool IsEmpty(int row, int col)
		{
			return gameboard[row, col].value == TileData.Value.Empty;
		}

		public int NumEmptyTiles()
		{
			return size * size - playerTiles;
		}
	}
}
