using Controllers;
using Objects;
using TMPro;
using UnityEngine;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
		protected override IHudView View => this;

		[SerializeField]
		private EndGameView _endGameView;

		public TextMeshProUGUI ScoreText;

		public void SetScore(int value)
		{
			ScoreText.text = value.ToString();
		}

		public IEndGameView EndGameView => _endGameView;
	}
}
