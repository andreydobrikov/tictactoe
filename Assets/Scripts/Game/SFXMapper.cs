using UnityEngine;

namespace com.tictactoe.game
{
	[CreateAssetMenu(fileName = "SFXMapper", menuName = "Sound/SFXMapper")]
	public class SFXMapper : ScriptableObject
	{
		public AudioClip tileClickSound;
		public AudioClip humanWinGameSound;
		public AudioClip humanLoseGameSound;
		public AudioClip gameTieGameSound;
		public AudioClip startGameSound;
	}
}
