using System;
using TMPro;
using UnityEngine;
using Controllers;

namespace Views
{
	public class EndGameView : BaseView<IEndGameView>, IEndGameView
	{
		protected override IEndGameView View => this;

		public TextMeshProUGUI ScoreText;
		public TextMeshProUGUI BestScore;

		public event Action ReplayEvent;

		public void SetScore(int value, int bestValue)
		{
			ScoreText.text = value.ToString();
			BestScore.text = bestValue.ToString();
		}

		public void ActionReplay()
		{
			ReplayEvent?.Invoke();
		}
	}
}
