using System;
using UnityEngine;
using Core;

namespace Controllers
{
	public interface IGameView
	{
		public event Action EndGameEvent;

		IEndGameView EndGameView { get; }

		void StartGame();
		void StopGame();
	}

	[CreateAssetMenu(menuName = "Game Proxy")]
	public class GameController : ScriptableObject, IGame
	{
		private IGameView _view;

		[SerializeField] private bool _resetOnNewGame = true;

		public event Action EndGameEvent;
		public event Action NewGameEvent;
		public event Action<int> ScoreChangedEvent;

		private int _score { get; set; }
		private int _best { get; set; }

		public void OnEnable()
		{
			if (_resetOnNewGame)
			{
				_score = 0;
			}
		}

		public void AddScore(int value)
		{
			_score += value;
			ScoreChangedEvent?.Invoke(_score);
		}

		public void ClearState()
		{
			_score = 0;
		}

		public int GetScore()
		{
			return _score;
		}

		public int GetBest()
		{
			return _best;
		}

		public void EndGame()
		{
			if (_score > _best)
			{
				_best = _score;
			}

			_view?.EndGameView?.Open(new EndGameController(this, _score));
			EndGameEvent?.Invoke();
		}

		public void NewGame()
		{
			NewGameEvent?.Invoke();
		}

		public void OnOpen(IGameView view)
		{
			view.EndGameEvent += EndGame;
		}

		public void OnClose(IGameView view)
		{
			view.EndGameEvent -= EndGame;
			_view = null;
		}
	}
}