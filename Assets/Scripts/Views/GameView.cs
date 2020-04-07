using System;
using Controllers;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class GameView : MonoBehaviour, IGameView
	{
		public GameController Controller;

		public GameObject[] GameObjects;

		public Spawner Spawner;

		[SerializeField] private EndGameView _endGameView;

		public Text ScoreText;

		public IEndGameView EndGameView => _endGameView;

		public event Action EndGameEvent;

		private void OnEnable()
		{
			Controller.OnOpen(this);
			Controller.ScoreChangedEvent += SetScore;
		}

		private void OnDisable()
		{
			Controller.ScoreChangedEvent -= SetScore;
			Controller.OnClose(this);
		}

		public void SetScore(int value)
		{
			ScoreText.text = value.ToString();
		}

		public void StartGame()
		{
			Spawner.GameOver += EndGame;
			foreach (var o in GameObjects)
			{
				o.SetActive(true); 
				
			}
		}

		public void StopGame()
		{
			Spawner.GameOver -= EndGame;
			foreach (var o in GameObjects)
				o.SetActive(false);
		}

		private void EndGame()
		{
			EndGameEvent?.Invoke();
		}
	}
}
