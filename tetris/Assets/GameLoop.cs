using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLoop : MonoBehaviour {

	GameController TerisGame = null;

	public Button bStart;
	public Button bRestart;

	public bool m_is_start = false;

	void Start () 
	{
		bStart.onClick.AddListener (start);
		bRestart.onClick.AddListener (restart);
	}

	// Update is called once per frame
	void Update ()
	{
		if (TerisGame != null) {
			// GameLoop
			// 前入
			TerisGame.HandlePlayerInput ();
			// 更新
			TerisGame.Update ();
			// 繪出 
			TerisGame.Draw ();
		}
	}

	void start()
	{
		if (!m_is_start) {
			TerisGame = new GameController (24, 12);
			m_is_start = true;
		}
	}
	void restart()
	{
		if (m_is_start) {
			TerisGame.Restart ();
		}
	}
}
