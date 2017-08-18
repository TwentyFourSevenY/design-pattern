using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake 
{
	//設Node共n個, 則Snake頭為第n個Node,Snake尾為第0個Node
	public List<Node> body=new List<Node>();
	public Game.direction_type direction;

	private static Snake snake = null; 
	private static Object obj = new Object();
	public static Snake Instance {
		get {
			if (snake == null)
				lock (obj) {
					if (snake == null) {
						snake = new Snake ();
					}
				}
			return snake;
		}
	}
	private Snake(){}

	//初始
	public void Init()
	{
		body.Clear ();
		//加入預設的節點
		Node node;
		for (int i = 0; i < 5; i++) {
			node = new Node ();
			node.row = 5;
			node.col = 3 + i;
			AddNode (node);
		}
		direction = Game.direction_type.RIGHT;
	}

	public void AddNode(Node node)
	{
		body.Add (node);
	}

	public Node GetFrontPosition()
	{
		Node front_position = new Node ();
		Node header;
		//取得蛇頭的位置
		header = body [body.Count - 1];
		front_position.Copy (header);

		//依方向決定下一個位置
		switch (direction) {
		case Game.direction_type.LEFT:
			front_position.col--;
			break;
		case Game.direction_type.RIGHT:
			front_position.col++;
			break;
		case Game.direction_type.UP:
			front_position.row--;
			break;
		case Game.direction_type.DOWN:
			front_position.row++;
			break;
		}
		return front_position;
	}

	//判斷某一點是否存在於m_snake中,用來判斷是否碰到自已用,true表示有碰到
	public bool IsHitBody(Node another_node)
	{
		foreach (var node in body) {
			if (Node.IsSameNode (node, another_node))
				return true;
		}
		return false;
	}

	//移動到下一個位置,指定Snake頭的位置
	public void MoveToNextPosition(Node header_node)
	{
		Node this_node = null;
		Node next_node = null;
		//移到前一個Node的位置上
		for (int i = 0; i < body.Count - 1; ++i) {
			this_node = body [i];
			next_node = body [i + 1];
			this_node.Copy (next_node);
		}
		 
		//Snake頭移至下一個位置上
		this_node = body [body.Count - 1];
		this_node.Copy (header_node);
	}

	public void Turn(Game.direction_type direction)
	{
		if (IsOppositeDirection (direction))
			return;
		this.direction = direction;
	}
	private bool IsOppositeDirection(Game.direction_type another)
	{
		return direction + 2 == another || direction == another + 2;
	}
}