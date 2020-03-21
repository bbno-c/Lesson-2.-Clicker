using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Proxy")]
public class GameProxy : ScriptableObject
{
	public int Score { get; private set; }

	[SerializeField] private bool _resetOnNewGame = true;

	public void OnEnable()
	{
		if (_resetOnNewGame)
		{
			Score = 0;
		}
	}

	public void AddScore()
	{
		Score++;
	}
}
