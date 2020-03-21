using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
	public GameProxy GameProxy;
	public Text ScoreText;
	public Text Best;

	private void Awake()
	{
		GameProxy.EndGameEvent += OnEndGame;
		gameObject.SetActive(false);
	}

	private void OnEndGame()
	{
		gameObject.SetActive(true);
		ScoreText.text = GameProxy.Score.ToString();
		Best.text = GameProxy.Best.ToString();
	}

	public void OnReplayClick()
	{
		gameObject.SetActive(false);
		GameProxy.NewGame();
	}
}
