namespace com.tictactoe.game
{
	public class MiniMaxEvaluator
	{
		private static int winScore = 10;
		private static int loseScore = -10;
		private static int tieScore = 0;

		public static TileData.Value playerTileValue;
		public static TileData.Value opponentTileValue;

		public static int Evaluate(BoardData board)
		{
			var winner = board.Winner();

			if (winner == playerTileValue)
				return winScore;

			else if (winner == opponentTileValue)
				return loseScore;

			return tieScore;
		}

		// This is the minimax function. It considers all
		// the possible ways the game can go and returns
		// the value of the board
		public static int Minimax(BoardData boardData, int depth, bool isMax, int alpha, int beta, int initialNumEmpty)
		{
			//we don't go beyond max score depth, this will prevent us from overblowing the stack
			//our lowest depth is 2, highest is 9, we check how many tiles are empty to calulate depth 
			//as we need to approximately calculate no more than 1m scenarios
			int depthTolerance = 9;

			if (initialNumEmpty > 99)
				depthTolerance = 2;
			if (initialNumEmpty > 21)
				depthTolerance = 3;
			else if (initialNumEmpty > 16)
				depthTolerance = 4;
			else if (initialNumEmpty > 12)
				depthTolerance = 5;
			else if (initialNumEmpty > 9)
				depthTolerance = 6;

			if (depth >= depthTolerance)
				return tieScore;

			int score = Evaluate(boardData);

			// If Maximizer has won the game
			// return his/her evaluated score and depth
			if (score == winScore)
				return score - depth;

			// If Minimizer has won the game
			// return his/her evaluated score and depth
			if (score == loseScore)
				return score + depth;

			// If there are no more moves and
			// no winner then it is a tie
			if (boardData.IsBoardFull())
				return tieScore;

			var tileData = boardData.tileData;

			// If this maximizer's move
			if (isMax)
			{
				int best = -1000;

				// Traverse all cells
				for (int i = 0; i < boardData.size; i++)
				{
					for (int j = 0; j < boardData.size; j++)
					{
						// Check if cell is empty
						if (tileData[i, j].value == TileData.Value.Empty)
						{
							// Make the move
							boardData.SetTileValue(i, j, playerTileValue);

							// Call minimax recursively and choose
							// the maximum value
							score = Minimax(boardData, depth + 1, !isMax, alpha, beta, initialNumEmpty);
							if (score > alpha)
							{
								alpha = score;
								best = score;
							}

							// Undo the move
							boardData.SetTileValue(i, j, TileData.Value.Empty);

							//cut off
							if (alpha >= beta)
							{
								break;
							}
						}
					}

					//cut off
					if (alpha >= beta)
					{
						break;
					}
				}

				return best;
			}

			// If this minimizer's move
			else
			{
				int best = 1000;

				// Traverse all cells
				for (int i = 0; i < boardData.size; i++)
				{
					for (int j = 0; j < boardData.size; j++)
					{
						// Check if cell is empty
						if (tileData[i, j].value == TileData.Value.Empty)
						{
							// Make the move
							boardData.SetTileValue(i, j, opponentTileValue);

							// Call minimax recursively and choose the minimum value
							score = Minimax(boardData, depth + 1, !isMax, alpha, beta, initialNumEmpty);
							if (score < beta)
							{
								beta = score;
								best = score;
							}

							// Undo the move
							boardData.SetTileValue(i, j, TileData.Value.Empty);

							//cut off
							if (alpha >= beta)
							{
								break;
							}
						}
					}

					//cut off
					if (alpha >= beta)
					{
						break;
					}
				}
				return best;
			}
		}

		// This will return the best possible
		// move for the player
		public static TilePosition FindBestMove(BoardData boardData, TileData.Value playerTile, TileData.Value opponentTile)
		{
			int bestVal = -1000;
			TilePosition bestMove = new TilePosition(-1, -1);
			playerTileValue = playerTile;
			opponentTileValue = opponentTile;

			var tileData = boardData.tileData;

			// Traverse all cells, evaluate minimax function
			// for all empty cells. And return the cell
			// with optimal value.
			for (int row = 0; row < boardData.size; row++)
			{
				for (int col = 0; col < boardData.size; col++)
				{
					// Check if cell is empty
					if (tileData[row, col].value == TileData.Value.Empty)
					{
						// Make the move
						boardData.SetTileValue(row, col, playerTileValue);

						// compute evaluation function for this move
						int moveVal = Minimax(boardData, 0, false, int.MinValue, int.MaxValue, boardData.NumEmptyTiles());

						// Undo the move
						boardData.SetTileValue(row, col, TileData.Value.Empty);

						// If the value of the current move is
						// more than the best value, then update
						// best
						if (moveVal > bestVal)
						{
							bestMove.row = row;
							bestMove.col = col;
							bestVal = moveVal;
						}
					}
				}
			}

			return bestMove;
		}
	}
}
