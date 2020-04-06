using System;
using UnityEngine.UI;
using Controllers;

namespace Views
{
	public class EndGameView : BaseView<IEndGameView>, IEndGameView
	{
		protected override IEndGameView View => this;

		public Text ScoreText;

		public event Action ReplayEvent;

		public void SetScore(int value)
		{
			ScoreText.text = value.ToString();
		}

		public void ActionReplay()
		{
			ReplayEvent?.Invoke();
		}
	}
}
