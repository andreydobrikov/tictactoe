namespace com.tictactoe.game
{
	//direct copy of protected UnityEngine.UI.Selectable
	public enum TileSelectionState
	{
		/// <summary>
		/// The UI object can be selected.
		/// </summary>
		Normal,

		/// <summary>
		/// The UI object is highlighted.
		/// </summary>
		Highlighted,

		/// <summary>
		/// The UI object is pressed.
		/// </summary>
		Pressed,

		/// <summary>
		/// The UI object is selected
		/// </summary>
		Selected,

		/// <summary>
		/// The UI object cannot be selected.
		/// </summary>
		Disabled,
	}
}
