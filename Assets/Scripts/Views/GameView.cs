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
		public GameObject SpawnerObject;

		[SerializeField]
		public MenuView _menuView;
		[SerializeField]
		private HudView _hudView;

		public IHudView HudView => _hudView;
		public IMenuView MenuView => _menuView;

		public event Action EndGameEvent;

		private void OnEnable()
		{
			Controller.OnOpen(this);
		}

		private void OnDisable()
		{
			Controller.OnClose(this);
		}

		public void StartGame()
		{
			Spawner.GameOver += EndGame;
			SpawnerObject.SetActive(true);
			foreach (var o in GameObjects)
			{
				o.SetActive(true); 
			}
		}

		public void StopGame()
		{
			Spawner.GameOver -= EndGame;
			SpawnerObject.SetActive(false);
			foreach (var o in GameObjects)
				o.SetActive(false);
		}

		private void EndGame()
		{
			EndGameEvent?.Invoke();
		}
	}
}
