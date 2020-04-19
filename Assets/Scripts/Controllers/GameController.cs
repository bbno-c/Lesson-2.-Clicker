using System;
using UnityEngine;
using Core;

namespace Controllers
{
	public interface IGameView
	{
		event Action EndGameEvent;

		IHudView HudView { get; }
		IMenuView MenuView { get; }

		void StartGame();
		void StopGame();
	}

	[CreateAssetMenu(menuName = "GameController")]
	public class GameController : ScriptableObject, IGame
	{
		private IGameView _view;

		[SerializeField] private bool _resetOnNewGame = true;

		public event Action EndGameEvent;
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

		public void NewGame()
		{
			_score = 0;
			_view?.HudView?.Open(new HudController(this));
			_view?.StartGame();
		}

		public void OnEndGame()
		{
			if (_score > _best)
			{
				_best = _score;
			}

			_view?.StopGame();
			_view?.HudView.EndGameView?.Open(new EndGameController(this, _score, _best));
			EndGameEvent?.Invoke();
		}

		public void OnOpen(IGameView view)
		{
			view.EndGameEvent += OnEndGame;
			view.MenuView?.Open(new MenuController(this));
			_view = view;
		}

		public void OnClose(IGameView view)
		{
			view.EndGameEvent -= OnEndGame;
			_view = null;
		}
	}
}