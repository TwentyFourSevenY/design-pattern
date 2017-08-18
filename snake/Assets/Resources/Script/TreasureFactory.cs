using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//三種工廠都繼承此介面工廠，三種工廠各自實作新增寶物的方法
interface TreasureFactory
{
	//這個
	Treasure CreateTreasure ();
}

//各自建立與回傳對應的實體，為何不用簡易工廠的switch，請上網找FactoryMethod
class AddNodeTreasureFactory : TreasureFactory
{
	public Treasure CreateTreasure()
	{
		return new AddNodeTreasure ();
	}
}

class SpeedUpTreasureFactory : TreasureFactory
{
	public Treasure CreateTreasure()
	{
		return new SpeedUpTreasure ();
	}
}

class SpeedDownTreasureFactory : TreasureFactory
{
	public Treasure CreateTreasure()
	{
		return new SpeedDownTreasure ();
	}
}

//如同RandomTreasure類別所述，這邊直接回傳使用RandomTreasure的靜態方法，因為該方法就是回傳隨機的實體
class RandomTreasureFactory : TreasureFactory
{
	public Treasure CreateTreasure()
	{
		return RandomTreasure.CreateRandomTreasure ();
	}
}