using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.tictactoe.game
{
	public class TileButton : Button
	{
		public event Action<TileSelectionState> OnTransition;
		public TileSelectionState tileSelectionState { get; private set; }

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);

			switch (state)
			{
				case SelectionState.Normal:
					tileSelectionState = TileSelectionState.Normal;
					OnTransition?.Invoke(TileSelectionState.Normal);
					break;
				case SelectionState.Highlighted:
					tileSelectionState = TileSelectionState.Highlighted;
					OnTransition?.Invoke(TileSelectionState.Highlighted);
					break;
				case SelectionState.Pressed:
					tileSelectionState = TileSelectionState.Pressed;
					OnTransition?.Invoke(TileSelectionState.Pressed);
					break;
				case SelectionState.Selected:
					tileSelectionState = TileSelectionState.Selected;
					OnTransition?.Invoke(TileSelectionState.Selected);
					break;
				case SelectionState.Disabled:
					tileSelectionState = TileSelectionState.Disabled;
					OnTransition?.Invoke(TileSelectionState.Disabled);
					break;
				default:
					break;
			}
		}
	}
}
