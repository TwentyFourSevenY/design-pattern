using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//三種寶物都繼承都繼承這個「寶物（共用的）」類別，此類別包含常用
public abstract class Treasure
{
	//enum，主要宣告有什麼樣的寶物類型
	public enum treasure_type {
		AddNode, SpeedUp, SpeedDown
	}

	//寶物的屬性，有節點跟型別
	public Node node{ get; private set; }
	public treasure_type type;

	//寶物的建構子，須由子類別用 :base() 呼叫，不然不會執行，沒有找到怎麼「強制」繼承者呼叫，但暫時不理會
	//這裡設為public沒有關係，因為整個Treasure是abstract（看上面第七行程式碼），不能被用來直接建立實體
	public Treasure()
	{
		node = GetRandomNode ();
		type = treasure_type.AddNode;
	}
	//寶物主要有一個方法，就是被吃到的時候，要做什麼事情，目前必須要傳Game進來，雖相當耦合，但暫時不理會
	public virtual void DoSomething(Game game){}

	//下面兩個方法是三個特別寫的，可以不必理會，功能為隨機出寶物的節點
	private static Node GetRandomNode()
	{
		return new Node (GetRandomRow (), GetRandomCol ());
	}
	private static int GetRandomRow()
	{
		return UnityEngine.Random.Range (0, Game.WEIGHT);
	}
	private static int GetRandomCol()
	{
		return UnityEngine.Random.Range (0, Game.HEIGHT);
	}
}

//以下三種寶物類別主要都有自己的建構子，用於設定對應的屬性（這邊僅有type）
//及覆寫DoSomething
class AddNodeTreasure : Treasure
{
	public AddNodeTreasure() : base()
	{
		type = treasure_type.AddNode;
	}
	public override void DoSomething(Game game)
	{
		Node snake_node = new Node ();
		snake_node.Copy (node);

		game.snake.AddNode (snake_node);

		Debug.Log ("增加節點");	
	}
}

class SpeedUpTreasure : Treasure
{
	public SpeedUpTreasure() : base()
	{
		type = treasure_type.SpeedUp;
	}
	public override void DoSomething(Game game)
	{
		game.SpeedUp ();
		Debug.Log ("加速");
	}
}

class SpeedDownTreasure : Treasure
{
	public SpeedDownTreasure() : base()
	{
		type = treasure_type.SpeedDown;
	}
	public override void DoSomething(Game game)
	{
		game.SpeedDown ();
		Debug.Log ("減速");
	}
}

//RandomTreasure比較特別，主要是將自己的建構子隱藏起來
//並且提供一個靜態CreateRandomTreasure方法，回傳隨機後的上面三種的其中一種實體
//其實也類似一個簡易工廠
class RandomTreasure : Treasure
{
	private RandomTreasure(){}
	public static Treasure CreateRandomTreasure()
	{
		Treasure treasure = null;
		treasure_type ty = GetRandomType ();
		switch (ty) {
		case treasure_type.AddNode:
			treasure = new AddNodeTreasure ();
			break;
		case treasure_type.SpeedUp:
			treasure = new SpeedUpTreasure ();
			break;
		case treasure_type.SpeedDown:
			treasure = new SpeedDownTreasure ();
			break;
		default:
			break;
		}
		return treasure;
	}
	//可以不用看，功能隨機出enum裡面的型別
	private static treasure_type GetRandomType()
	{
		Debug.Log (Enum.GetNames (typeof(treasure_type)).Length);
		return (treasure_type)UnityEngine.Random.Range (0, Enum.GetNames (typeof(treasure_type)).Length);
	}
}