using System;
using Core;

namespace Controllers
{
	public interface IEndGameView : IView
	{
		event Action ReplayEvent;

		void SetScore(int value);
	}

	public class EndGameController : IController<IEndGameView>
	{
		private readonly IGame _gameProxy;

		private IEndGameView _view;

		private readonly int _score;

		public EndGameController(IGame game, int score)
		{
			_gameProxy = game;
			_score = score;
		}

		public void OnOpen(IEndGameView view)
		{
			view.SetScore(_score);
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