using com.tictactoe.dispatcher;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.tictactoe.game
{
	public class TileRenderer : MonoBehaviour
	{
		[SerializeField] private GameObject tileO;
		[SerializeField] private GameObject tileX;
		[SerializeField] private GameObject ghostTileO;
		[SerializeField] private GameObject ghostTileX;
		[SerializeField] private GameObject tileEmpty;
		[SerializeField] private TileButton button;
		[SerializeField] private AnimationCurve animationCurve;

		public TileData tileData { get; private set; }
		public TilePosition tilePosition { get; private set; }

		// Declare the float to use with the animation curve
		private float curveDeltaTime = 0.0f;
		private float speed = 1.0f;
		private Coroutine animCoroutine;
		private GameObject ghostTile;

		// Complete the update code 
		private IEnumerator AnimateHighlight()
		{
			// Get the current position of the sphere
			Vector3 scale = ghostTile.transform.localScale;

			// Call evaluate on that time   
			curveDeltaTime += Time.deltaTime;
			scale.x = animationCurve.Evaluate(curveDeltaTime);
			scale.y = animationCurve.Evaluate(curveDeltaTime);

			// Update the current position of the sphere
			ghostTile.transform.localScale = scale;
			yield return null;
			if (curveDeltaTime > 1.0f)
			{
				curveDeltaTime = 0.0f;
			}

			//if we no longer in highlight - break, else continue
			if (animCoroutine == null || button.tileSelectionState != TileSelectionState.Highlighted)
				yield break;

			animCoroutine = StartCoroutine(AnimateHighlight());
		}

		private void Awake()
		{
			ghostTile = ghostTileX;

			button?.onClick.AddListener(ClickTile);
			button.OnTransition += OnTransition;

			GlobalEventDispatcher.Instance.dispatcher.AddListener<SetPlayerEvent>(OnPlayerChanged);
		}


		private void OnDestroy()
		{
			button?.onClick.RemoveListener(ClickTile);
			button.OnTransition -= OnTransition;
			GlobalEventDispatcher.Instance?.dispatcher.RemoveListener<SetPlayerEvent>(OnPlayerChanged);
		}

		private void OnPlayerChanged(SetPlayerEvent evt)
		{
			ghostTile = evt.model.player.tile == TileData.Value.X ? ghostTileX : ghostTileO;
		}

		private void OnTransition(TileSelectionState state)
		{
			switch (state)
			{
				case TileSelectionState.Normal:
					ghostTile.SetActive(false);
					if (animCoroutine != null) StopCoroutine(animCoroutine);
					animCoroutine = null;
					break;

				case TileSelectionState.Highlighted:
					ghostTile.SetActive(true);
					animCoroutine = StartCoroutine(AnimateHighlight());
					break;

				case TileSelectionState.Pressed:
					ghostTile.SetActive(false);
					if (animCoroutine != null) StopCoroutine(animCoroutine);
					animCoroutine = null;
					break;

				case TileSelectionState.Selected:
					ghostTile.SetActive(false);
					if (animCoroutine != null) StopCoroutine(animCoroutine);
					animCoroutine = null;
					break;

				case TileSelectionState.Disabled:
					ghostTile.SetActive(false);
					if (animCoroutine != null) StopCoroutine(animCoroutine);
					animCoroutine = null;
					break;

				default:
					break;
			}
		}

		private void ClickTile()
		{
			TileClickedEventModel tileClickedEventModel = new TileClickedEventModel(this);
			TileClickedEvent evt = EventFactory.Instance.Create<TileClickedEvent, TileClickedEventModel>(tileClickedEventModel);
			GlobalEventDispatcher.Instance.dispatcher.Dispatch(evt);
		}

		public void Render(TileData.Value value, int row, int col)
		{
			tileData = new TileData();
			tileData.value = value;
			tilePosition = new TilePosition(col, row);

			tileO.SetActive(tileData.IsO);
			tileX.SetActive(tileData.IsX);
			button.enabled = tileData.IsEmpty;
		}
	}
}
