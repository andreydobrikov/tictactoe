using UnityEngine;

namespace com.tictactoe.game
{
	[CreateAssetMenu(fileName = "Row", menuName = "Board/RowConfig")]
	public class RowConfig : ScriptableObject
	{
		public TileData[] tiles;
	}
}
