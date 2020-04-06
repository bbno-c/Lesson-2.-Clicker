using System;
using UnityEngine;
using Core;

namespace Controllers
{
	interface IGameView : IView
	{
		event Action PlayerDeadEvent;
		event Action<float> PlayerHealthChangeEvent;

		void StartGame();
		void StopGame();

		IHudView HudView { get; }
		IMenuView MenuView { get; }
	}

	[CreateAssetMenu(menuName = "Game Proxy")]
	public class GameController : ScriptableObject, IGame
	{
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
			EndGameEvent?.Invoke();
		}

		public void NewGame()
		{
			NewGameEvent?.Invoke();
		}
	}
}