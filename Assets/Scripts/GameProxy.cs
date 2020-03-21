using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Proxy")]
public class GameProxy : ScriptableObject
{
	public event Action EndGameEvent;
	public event Action NewGameEvent;
	public int Score { get; private set; }
	public int Best { get; private set; }
	[SerializeField] private bool _resetOnNewGame = true;

	public void OnEnable()
	{
		if (_resetOnNewGame)
		{
			Score = 0;
		}
	}

	public void ClearState()
	{
		Score = 0;
	}

	public void AddScore()
	{
		Score++;
	}

	public void EndGame()
	{
		if (Score>Best)
		{
			Best = Score;
		}
		EndGameEvent?.Invoke();
	}

	public void NewGame()
	{
		NewGameEvent?.Invoke();
	}
}
