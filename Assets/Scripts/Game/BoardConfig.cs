using UnityEngine;

namespace com.tictactoe.game
{
	[CreateAssetMenu(fileName = "BoardConfig", menuName = "Board/BoardConfig")]
	public class BoardConfig : ScriptableObject
	{
		public RowConfig[] rows;
	}
}
