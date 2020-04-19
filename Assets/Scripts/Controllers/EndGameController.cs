using System;
using Core;

namespace Controllers
{
	public interface IEndGameView : IView
	{
		event Action ReplayEvent;

		void SetScore(int value, int bestValue);
	}

	public class EndGameController : IController<IEndGameView>
	{
		private readonly IGame _gameProxy;

		private IEndGameView _view;

		private readonly int _score;
		private readonly int _bestScore;

		public EndGameController(IGame game, int score, int bestScore)
		{
			_gameProxy = game;
			_score = score;
			_bestScore = bestScore;
		}

		public void OnOpen(IEndGameView view)
		{
			view.SetScore(_score, _bestScore);
			view.ReplayEvent += OnReplay;
			_view = view;
		}

		public void OnClose(IEndGameView view)
		{
			view.ReplayEvent -= OnReplay;
			_view = null;
		}

		public void OnReplay()
		{
			_view?.Close(this);
			_gameProxy.NewGame();
		}
	}
}