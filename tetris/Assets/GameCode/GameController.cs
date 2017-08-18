using UnityEngine;
using System.Collections;

using tetris;

public class GameController
{
	public GameController(int height, int width)
	{
		HEIGHT = height <= 28 ? height : 28;
		WIDTH = width <= 14 ? width : 14;
		CONTAINER_SIZE = HEIGHT * WIDTH;


		// 遊戲物件
		m_tetris = new Tetris (HEIGHT, WIDTH);
		// 載入block原型
		m_blockArray = new UnityEngine.Object[8];
		LoadBlockPrefabe ();
		m_unity_container = new GameObject[CONTAINER_SIZE];
		m_unity_next_block = new GameObject[4 * 4];
		m_now_used_block_count = 0;

		m_move_to_left = false;
		m_move_to_right = false;
		m_rotate = false;
		m_move_to_down = false;
		m_game_over = false;
	}

	// 接收玩家輪入
	public void HandlePlayerInput()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space))
			m_rotate = true;


		if (Input.GetKey (KeyCode.DownArrow))
			m_move_to_down = true;
		if (Input.GetKey (KeyCode.LeftArrow))
			m_move_to_left = true;
		if (Input.GetKey (KeyCode.RightArrow))
			m_move_to_right = true;
	}

	//更新
	public void Update ()
	{
		if (m_tetris.IsGameOver)
			return;

		ManualUpdate();	//手動落下、旋轉或移動
		AutoUpdate ();	//自動落下
	}

	// 顯示
	public void Draw()
	{
		if (m_game_over)
			return;

		m_now_used_block_count = 0;
		Clear ();
		DrawContainer ();
		DrawNextBlock ();
	}

	public void Restart()
	{
		ClearContainer ();
		m_tetris = new Tetris (HEIGHT, WIDTH);
		m_game_over = false;
		m_now_used_block_count = 0;
	}

	readonly int HEIGHT;
	readonly int WIDTH;
	readonly int CONTAINER_SIZE;

	bool m_game_over;
	bool m_move_to_left;
	bool m_move_to_right;
	bool m_move_to_down;
	bool m_rotate;
	int m_control_timer = 0;
	int m_auto_fall_time = 0;
	readonly int MANUAL_INTERVAL = 50;
	readonly int AUTO_INTERVAL = 500;
	Tetris m_tetris;

	UnityEngine.Object[] m_blockArray;
	GameObject[] m_unity_container;
	GameObject[] m_unity_next_block;
	int m_now_used_block_count;

	void Clear ()
	{
		ClearContainer ();
		ClearNextBlock ();
	}

	void ManualUpdate ()
	{
		m_control_timer += (int)(Time.deltaTime * 1000);
		if (m_control_timer > MANUAL_INTERVAL) {
			if (m_move_to_right)
				m_tetris.MoveBlock (EM_MOVE_METHOD.RIGHT);
			if (m_move_to_left)
				m_tetris.MoveBlock (EM_MOVE_METHOD.LEFT);
			if (m_rotate)
				m_tetris.MoveBlock (EM_MOVE_METHOD.ROTATE);
			if (m_move_to_down)
				m_tetris.MoveBlock (EM_MOVE_METHOD.DOWN);
			m_move_to_right = false;
			m_move_to_left = false;
			m_move_to_down = false;
			m_rotate = false;
			m_control_timer = 0;
		}
	}

	void AutoUpdate()
	{
		m_auto_fall_time += (int)(Time.deltaTime * 1000);
		if (m_auto_fall_time >= AUTO_INTERVAL) {
			m_tetris.MoveBlock (EM_MOVE_METHOD.DOWN);
			m_auto_fall_time = 0;
		}
	}

	void ClearContainer ()
	{
		for (int i = 0; i < CONTAINER_SIZE; ++i) {
			if (m_unity_container [i]) {
				GameObject.Destroy (m_unity_container [i]);
				m_unity_container [i] = null;
			}
		}
	}

	void ClearNextBlock ()
	{
		for (int i = 0; i < 16; ++i) {
			if (m_unity_next_block [i]) {
				GameObject.Destroy (m_unity_next_block [i]);
				m_unity_next_block [i] = null;
			}
		}
	}

	void DrawContainer ()
	{
		//顯示container
		for (int i = 0; i < HEIGHT; i++) {
			for (int j = 0; j < WIDTH; j++) {
				int BlockValue = m_tetris.ContainerBody [i, j];
				if (BlockValue != 0) {
					DrawDifferentColor (BlockValue, i, j);
				}
			}
		}
	}

	void DrawNextBlock ()
	{
		//顯示next block2
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				int BlockValue = m_tetris.ContainerNext [i, j];
				if (BlockValue != 0)
					DrawDifferentColor (BlockValue, i, j + 15);
			}
		}
	}

    void DrawDifferentColor(int BlockValue,int Row,int Col)
	{
		//Row * BlockContainer.C_WIDTH + Col;
		int Pos = m_now_used_block_count;
		m_now_used_block_count++;

		if (BlockValue == 0)
			return;

		m_unity_container [Pos] = UnityEngine.Object.Instantiate (m_blockArray [BlockValue]) as GameObject;
		if (m_unity_container [Pos] == null) {
			Debug.Log ("生成物件錯誤");
			return;
		}

		// 設定位置
		m_unity_container [Pos].transform.position = new Vector2 (Col, HEIGHT - Row);
	}

	// 載入block原型
	void LoadBlockPrefabe()
	{
		// 動態產生原件,物件放在 \Assets\Resources\Block\ 目錄下的BlueBlock Pr
//		string[] BlockPath = new string[8] {"",
//			"Block/BlueBlock",
//			"Block/GreenBlock",
//			"Block/OrangeBlock",
//			"Block/PinkBlock",
//			"Block/PurpleBlock",
//			"Block/RedBlock",
//			"Block/YellowBlock"
//		};
		string[] BlockPath = new string[8] {
			"",
			"Block/BlockBlue",
			"Block/BlockGreen",
			"Block/BlockOrange",
			"Block/BlockPink",
			"Block/BlockPurple",
			"Block/BlockWhite",
			"Block/BlockYellow"
		};

		for (int i = 1; i < 8; ++i) {
			m_blockArray [i] = Resources.Load (BlockPath [i]);
			if (m_blockArray [i] == null) {
				Debug.Log ("Load " + BlockPath [i] + " error!");
			}
		}

	}
}
