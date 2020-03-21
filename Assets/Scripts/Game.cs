using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public GameProxy GameProxy;
	public GameObject spawner;

	private void OnEnable()
	{
		GameProxy.NewGameEvent += OnNewGame;
	}

	private void OnDisable()
	{
		GameProxy.NewGameEvent -= OnNewGame;
	}

	private void OnNewGame()
	{
		GameProxy.ClearState();
		spawner.SetActive(true);
	}
}
