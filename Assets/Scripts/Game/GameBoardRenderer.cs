using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.tictactoe.game
{
	[CreateAssetMenu(fileName = "GameBoardRenderer", menuName = "Renderers/GameBoardRenderer")]
	public class GameBoardRenderer : BoardRenderer
	{
		[SerializeField] private GameObject tilePrefab;
		[SerializeField] private GameObject tileContainer;

		private GameObject tileContainerObject;
		private GameObject TileContainerObject
		{
			get
			{
				if (tileContainerObject == null)
				{
					tileContainerObject = Instantiate(tileContainer);
				}

				return tileContainerObject;
			}
		}

		private TileRenderer[,] tileRenderers;

		public override void Render(BoardData board)
		{
			if (tileRenderers == null)
				tileRenderers = new TileRenderer[board.size, board.size];

			//set the size of the grid
			GridLayoutGroup layout = TileContainerObject.GetComponent<GridLayoutGroup>();
			layout.constraintCount = board.size;

			for (int row = 0; row < board.size; row++)
			{
				for (int col = 0; col < board.size; col++)
				{
					RenderTileValue(row, col, board.GetTileValue(row, col), board);
				}
			}
		}

		public override void RenderTileValue(int row, int col, TileData.Value val, BoardData board)
		{
			if (tileRenderers[row, col] == null)
			{
				GameObject tile = Instantiate(tilePrefab, TileContainerObject.transform);
				TileRenderer tileRenderer = tile.GetComponent<TileRenderer>();
				tileRenderers[row, col] = tileRenderer;
				tile.name = "Tile - row: " + row + " col:" + col;
			}

			tileRenderers[row, col].Render(val, row, col);
		}
	}
}
