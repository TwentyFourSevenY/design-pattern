using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
	public enum direction_type {UP=1, RIGHT=2, DOWN=3, LEFT=4}//WARNING: Snake.IsOpposite method is sensitive order of elems
	public const int RS_OK = 0;
	public const int RS_FAIL = 1;
	public const int RS_STAGE_CLEAR = 2;
	public const int WEIGHT = 20;
	public const int HEIGHT = 20;
	public Zone zone = Zone.Instance;
	public Snake snake = Snake.Instance;
	public List<Treasure> treasures = new List<Treasure>();
    public const int MAX_SPEED_LV = 15;

	private int move_speed;
	private int move_speed_lv;
	private int update_timer=0;
	private int move_time_new;
	private int move_time_old;
	private int score;
    
    public Game(){}

    //開始遊戲
    public void InitGame()
    {
        treasures.Clear ();
        snake.Init ();
        score = 0;

        //加入寶物
        AddTreasure ();

        //設定速度
        SetMoveSpeedLevel (9);
    }

    //執行GameLoop
	public int DoGameLoop(direction_type key_direction)
    {
        //先執行動作
		snake.Turn(key_direction);

        //自動移動
        update_timer += (int)(Time.deltaTime * 1000);

        //時間沒到
        if (update_timer < move_speed)
            return RS_OK;
        update_timer = 0;

        //判斷是否還能移動
        if (!MoveToNextNode ()) {
            Debug.LogWarning (" Game Over!");
            return RS_FAIL;
        }

        // TODO: 是否增加速度

        // TODO: 計算分數

        // TODO: 是否過關
        return RS_OK;
    }

	//設定移動速度等級(以程式內預定)

	public void SpeedUp()
	{
		SetMoveSpeedLevel (move_speed_lv + 1);
	}
	public void SpeedDown()
	{
		SetMoveSpeedLevel (move_speed_lv - 1);
	}

	private void SetMoveSpeedLevel(int lv)
	{
		move_speed_lv = lv;
		if (move_speed_lv > MAX_SPEED_LV)
			move_speed_lv = MAX_SPEED_LV;
		if (move_speed_lv < 1)
			move_speed_lv = 1;
		move_speed = 850 - move_speed_lv * 50;
	}

    //移到下一個節點
	private bool MoveToNextNode()
    {
        //取得下一格的位置
        Node next_node = snake.GetFrontPosition ();
    
        if (snake.IsHitBody (next_node))
            return false;
        if (zone.IsHitZone (next_node))
            return false;
        if (CheckHitTreasure (next_node))
            score += 100;
    
        snake.MoveToNextPosition (next_node);
    
        //計算分數(速度*Node數)
        //m_score = ?;

        return true;
    }

    //增加寶物
	private void AddTreasure()
	{
		//當蛇身越來越長，for迴圈效率降低，後續寶物可以改以隨機「未使用節點」
		for (int i = 0; i < 3; i++) {
			if (snake.body.Count == WEIGHT * HEIGHT) {
				break;
			} else {
				TreasureFactory factory = new RandomTreasureFactory();
				EnsureNoHitBody (factory);
			}
		}
    }
	private void EnsureNoHitBody(TreasureFactory factory)
	{
		Treasure treasure = factory.CreateTreasure ();
		if (!snake.IsHitBody (treasure.node)) {
			treasures.Add (treasure);
		} else {
			EnsureNoHitBody (factory);
		}
	}

    //判斷有沒有吃到寶物
	private bool CheckHitTreasure(Node node)
	{
		bool is_eat = false;
		foreach (var item in treasures) {
			if (Node.IsSameNode (item.node, node)) {
				EatTreasure (item);
				is_eat = true;
				break;
			}
		}

        // 重新加入
        if (treasures.Count == 0)
            AddTreasure ();
        return is_eat;
    }
	private void EatTreasure(Treasure treasure)
	{
		treasure.DoSomething (this);
		treasures.Remove (treasure);
	}
}