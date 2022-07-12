using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace com.tictactoe.game
{
	public class UIController : MonoBehaviour
	{
		[SerializeField] private Button startButton;
		[SerializeField] private Canvas winnerCanvas;
		[SerializeField] private Canvas startGameCanvas;
		[SerializeField] private TextMeshProUGUI winnerText;

		[SerializeField] private GameStateController gameController;

		private void Start()
		{
			startButton.onClick.AddListener(LoadGameCommand);
		}

		private void OnDestroy()
		{
			startButton.onClick.RemoveListener(LoadGameCommand);
		}

		private void LoadGameCommand()
		{
			gameController.ResetGame();
		}

		public void ResetGame()
		{
			startGameCanvas.gameObject.SetActive(false);
		}

		public IEnumerator SetEndGame(string endGameText)
		{
			winnerCanvas.gameObject.SetActive(true);
			winnerText.text = endGameText;
			yield return new WaitForSeconds(3f);
			winnerCanvas.gameObject.SetActive(false);
			startGameCanvas.gameObject.SetActive(true);
		}
	}
}
