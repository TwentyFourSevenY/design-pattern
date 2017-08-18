using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	private Game snake_game = new Game();
	private int game_status = 0;
	//由上列判斷key的動作
	private Game.direction_type key_direction;

	// 第0個不算
	private UnityEngine.Object[] block_array;
	private int container_size;
	private GameObject[] unity_container;
	private int now_used_block_count = 0;
	
	// Use this for initialization
	void Start ()
	{
		// 載入block原型
		block_array = new UnityEngine.Object[8];
		LoadBlockPrefabe ();

		container_size = Game.WEIGHT * Game.HEIGHT;
		unity_container = new GameObject[container_size];

		// 開始
		snake_game.InitGame ();
		game_status = 1;
	}

	// 載入block原型
	void LoadBlockPrefabe()
	{
		// 動態產生原件,物件放在 \Assets\Resources\Block\ 目錄下的BlueBlock Pr
		string[] BlockPath = new string[8] {"",
			"Block/BlueBlock", 
			"Block/GreenBlock", 
			"Block/OrangeBlock",
			"Block/PinkBlock",
			"Block/PurpleBlock",
			"Block/RedBlock",
			"Block/YellowBlock"
		};

		for (int i = 1; i < 8; ++i) {
			block_array [i] = Resources.Load (BlockPath [i]);
			if (block_array [i] == null) {
				print ("載入" + BlockPath [i] + "錯誤");
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (game_status != 1)
			return;

		// 輸入
		PlayerInput ();

		// 執行 
		snake_game.DoGameLoop (key_direction);

		Show ();
	}

	// 玩家輸入
	void PlayerInput()
	{
		if (Input.GetKeyUp (KeyCode.DownArrow))
			key_direction = Game.direction_type.DOWN;
		if (Input.GetKeyUp (KeyCode.UpArrow))
			key_direction = Game.direction_type.UP;
		if (Input.GetKeyUp (KeyCode.RightArrow))
			key_direction = Game.direction_type.RIGHT;
		if (Input.GetKeyUp (KeyCode.LeftArrow))
			key_direction = Game.direction_type.LEFT;
	}

	void Show()
	{
		ClearContainer ();
		RenderSnake ();
		RenderTreasure ();
	}

	void ClearContainer()
	{
		for (int i = 0; i < container_size; ++i) {
			if (unity_container [i]) {
				Destroy (unity_container [i]);
				unity_container [i] = null;
			}
		}
		now_used_block_count = 0;
	}

	// 顯示Snake
	void RenderSnake()
	{
		int size = snake_game.snake.body.Count;
		for (int i = 0; i < size; i++) {
			Node theNode = snake_game.snake.body [i];
			ShowNode (2, theNode.row, theNode.col);
		}
	}

	// 顯示Treasure
	void RenderTreasure()
	{
		int size = snake_game.treasures.Count;
		for (int i = 0; i < size; i++) {
			Treasure treasure = snake_game.treasures [i];
			Node node = treasure.node;
			ShowNode ((int)treasure.type + 3, node.row, node.col);
		}
	}
	
	// 顯示節點
	void ShowNode(int BlockValue, int Row, int Col)
	{
		int Pos = ++now_used_block_count;
		if (BlockValue == 0)
			return;

		unity_container [Pos] = UnityEngine.Object.Instantiate (block_array [BlockValue]) as GameObject;
		if (unity_container [Pos] == null) {
			print ("生成物件錯誤");
			return;
		}

		// 設定位置
		unity_container [Pos].transform.position = new Vector3 (Col, Game.WEIGHT - Row, 0);
	}
}