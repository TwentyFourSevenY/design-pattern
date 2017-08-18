﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
// 賓果Ai
public class OtherAiBingoBoard : BingoBoard
{
	protected int[] m_LinePoint = new int[12];

	public OtherAiBingoBoard()
	{

	}

	// 決定出牌
	public int GetNextNumber()
	{
		int[,] point = new int[5, 5];
		int NextNumber = -1;
		int col = 5;
		int row = 5;
		//計算每個位置價值(point)
		for (int c = 0; c < 5; c++)
		{
			for (int r = 0; r < 5; r++)
			{
				point[c, r] = 0;
				if (m_Board[c, r] == 0)
					continue;
				else if (c == 2 && c == r)//在正中央的交點
				{
					for (int k = 0; k < 5; k++)
					{
						if (m_Board[c, k] == 0)
							point[c, r] += 1;
						if (m_Board[k, r] == 0)
							point[c, r] += 1;
						if (m_Board[k, k] == 0)
							point[c, r] += 1;
						if (m_Board[k, 4 - k] == 0)
							point[c, r] += 1;
					}

					point[c, r] += 12;//交點加權12
				}
				else if (c == row || (c + r) == 4)//在斜線上的點
				{
					for (int k = 0; k < 5; k++)
					{
						if (m_Board[c, k] == 0)
							point[c, r] += 1;
						if (m_Board[k, r] == 0)
							point[c, r] += 1;
						if (m_Board[k, k] == 0)
							point[c, r] += 1;
						if (m_Board[k, 4 - k] == 0)
							point[c, r] += 1;
					}
					point[c, r] += 8;//斜線上加權8
				}
				else
				{
					for (int k = 0; k < 5; k++)
					{
						if (m_Board[c, k] == 0)
							point[c, r] += 1;
						if (m_Board[k, r] == 0)
							point[c, r] += 1;
					}
				}

			}
		}
		//找出最有價值的選擇
		int MaxPrice = -1;
		int MaxPriceNumber = -1;
		for (int c = 0; c < 5; c++)
		{
			for (int r = 0; r < 5; r++)
			{
				if (point[c, r] > MaxPrice)
				{
					MaxPrice = point[c, r];
					MaxPriceNumber = m_Board[c, r];
				}
			}
		}

		NextNumber = MaxPriceNumber;


		/*
        int lineindex = 0;
        int point = 0;

        //計算列值
        for (int i = 0; i < 5; i++)
        {
            point = 0;
            for (int j = 0; j < 5; j++)
                if (m_Board[i, j] > 0)
                    point++;
            m_LinePoint[lineindex++] = point;
        }

        //計算行值
        for (int j = 0; j < 5; j++)
        {
            point = 0;
            for (int i = 0; i < 5; i++)
                if (m_Board[i, j] > 0)
                    point++;
            m_LinePoint[lineindex++] = point;
        }

        // TODO:左斜,右斜
        for (int i = 0; i < 5; i++)
        {
            point = 0;
            if (m_Board[i, i] > 0)
                point++;
        }
        m_LinePoint[lineindex++] = point;


        for (int i = 4; i >= 0; i--)
        {
            point = 0;
            if (m_Board[i, 4 - i] > 0)
                point++;
        }
        m_LinePoint[lineindex++] = point;

        // 取得最少分數者
        int MinNum = 6;
        int MinIndex = -1;
        for (lineindex = 0; lineindex < 12; ++lineindex)
            if (m_LinePoint [lineindex] != 0 && m_LinePoint [lineindex] < MinNum) 
            {
                MinNum = m_LinePoint [lineindex];
                MinIndex = lineindex;
            }

        // 決定出牌,列方向
        int NextNumber = 0;
        if( MinIndex<5 )
        {
            // 第一個號碼
            for (int j = 0; j < 5; j++)
                if (m_Board [MinIndex, j] != 0) {
                    NextNumber = m_Board [MinIndex, j];
                    break;
                }
        }
        // 決定出牌,行方向
        else if(MinIndex <10)
        {
            // 第一個號碼
            for (int i = 0; i < 5; i++)
                if (m_Board [i, MinIndex - 5] != 0) {
                    NextNumber = m_Board [i, MinIndex - 5];
                    break;
                }
        }
        // TODO,左斜(左上->右下)
        else if(MinIndex == 10)
        {
            for(int i = 0; i < 5; i++)
            {
                if(m_Board[i,i]!= 0)
                {
                    NextNumber = m_Board[i, i];
                    Debug.Log("電腦選擇左斜");
                    break;
                }
            }
        }
        // TODO,右斜(右上->左下)
        else
        {
            for(int i = 4;i>=0;i--)
                if (m_Board[i, 4 - i] != 0)
                {
                    NextNumber = m_Board[i, 4 - i];
                    Debug.Log("電腦選擇右斜");
                    break;
                }
        }

		//Debug.Log("電腦出牌[" + NextNumber + "]" + "價值 " + MaxPrice);
		return NextNumber;
	}
}*/

		/*
public class OtherAiBingoBoard : BingoBoard
{
	public OtherAiBingoBoard()
	{
	}

	const int bound = 5;
	int[,] pointValue = new int[bound, bound];
	int HighReturnOnInvestmentAddress = -1;

	public int GetNextNumber()
	{
		Reset ();
		CalcValue ();
		UseHighestValueAsAddress ();

		//Debug.Log ("Ai1出牌[" + HighReturnOnInvestmentAddress + "]");
		return HighReturnOnInvestmentAddress;
	}

	void Reset()
	{
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				pointValue [c, r] = 0;
			}
		}
	}

	void CalcValue()
	{
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (m_Board [c, r] == 0) {
					addCol (c);
					addRow (r);
					addSlash (c, r);
					addBackSlash (c, r);
				}
			}
		}

		//weighted
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (c == r) {
					pointValue [c, r] += 4;
				}
			}
		}
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if ((bound-c-1) == r) {
					pointValue [c, r] += 4;
				}
			}
		}

		//
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (m_Board [c, r] == 0) {
					pointValue [c, r] = 0;
				}
			}
		}
	}
	void addCol (int col)
	{
		for (int r = 0; r < bound; r++) {
			pointValue [col, r]++;
		}
	}
	void addRow(int row)
	{
		for (int c = 0; c < bound; c++) {
			pointValue [c, row]++;
		}
	}
	void addSlash(int col, int row)
	{
		if (col == row) {
			for (int c = 0; c < bound; c++) {
				for (int r = 0; r < bound; r++) {
					if (c == r) {
						pointValue [c, r]++;
					}
				}
			}
		}
	}
	void addBackSlash(int col, int row)
	{
		if ((bound - col - 1) == row) {
			for (int c = 0; c < bound; c++) {
				for (int r = 0; r < bound; r++) {
					if ((bound-c-1) == r) {
						pointValue [c, r]++;
					}
				}
			}
		}
	}

	void UseHighestValueAsAddress()
	{
		int value = 0;
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (pointValue [c, r] > value) {
					value = pointValue [c, r];
					HighReturnOnInvestmentAddress = m_Board [c, r];
				}
			}
		}
	}
}
		*/
		using System.Collections;
		using System.Collections.Generic;
		using UnityEngine;

		// 賓果Ai
		public class OtherAiBingoBoard : BingoBoard
		{
			protected int[] m_LinePoint = new int[12];

			public OtherAiBingoBoard()
			{

			}

			// 決定出牌
			public int GetNextNumber()
			{
				int[,] point = new int[5, 5];
				int NextNumber = -1;
				int col = 5;
				int row = 5;
				int bound = 5; //正方形邊長
				//計算每個位置價值(point)
				for (int c = 0; c < bound; c++)
				{
					for (int r = 0; r < bound; r++)
					{
						point[c, r] = 0;
						if (m_Board[c, r] == 0)//如果已選過則不用判斷
							continue;
						for (int k = 0; k < bound; k++)
						{
							if (m_Board[c, k] == 0) point[c, r] += 1;//判斷row方向之已選過的點 
							if (m_Board[k, r] == 0) point[c, r] += 1;//判斷col方向之已選過的點
							if (c == r || (c + r) == (bound-1))//如果為斜線上的點
							{
								if (c == r)//左上向右下的斜線
								if (m_Board[k, k] == 0) point[c, r] += 2;
								if ((c + r) == (bound-1))//左下向右上的斜線
								if (m_Board[k, (bound-1) - k] == 0) point[c, r] += 2;

							}
						}
						if (c == r || (c + r) == (bound-1)) point[c, r] += 8;//斜線上的點加權8
						if (c == r && (c + r) == (bound-1)) point[c, r] += 4;//中心交點 額外加權 4


					}
				}
				//找出最有價值的選擇
				int MaxPrice = -1;
				int MaxPriceNumber = -1;
				for (int c = 0; c < 5; c++)
				{
					for (int r = 0; r < 5; r++)
					{
						if (point[c, r] > MaxPrice)
						{
							MaxPrice = point[c, r];
							MaxPriceNumber = m_Board[c, r];
						}
					}
				}

				NextNumber = MaxPriceNumber;


				/*
        int lineindex = 0;
        int point = 0;

        //計算列值
        for (int i = 0; i < 5; i++)
        {
            point = 0;
            for (int j = 0; j < 5; j++)
                if (m_Board[i, j] > 0)
                    point++;
            m_LinePoint[lineindex++] = point;
        }

        //計算行值
        for (int j = 0; j < 5; j++)
        {
            point = 0;
            for (int i = 0; i < 5; i++)
                if (m_Board[i, j] > 0)
                    point++;
            m_LinePoint[lineindex++] = point;
        }

        // TODO:左斜,右斜
        for (int i = 0; i < 5; i++)
        {
            point = 0;
            if (m_Board[i, i] > 0)
                point++;
        }
        m_LinePoint[lineindex++] = point;


        for (int i = 4; i >= 0; i--)
        {
            point = 0;
            if (m_Board[i, 4 - i] > 0)
                point++;
        }
        m_LinePoint[lineindex++] = point;

        // 取得最少分數者
        int MinNum = 6;
        int MinIndex = -1;
        for (lineindex = 0; lineindex < 12; ++lineindex)
            if (m_LinePoint [lineindex] != 0 && m_LinePoint [lineindex] < MinNum) 
            {
                MinNum = m_LinePoint [lineindex];
                MinIndex = lineindex;
            }

        // 決定出牌,列方向
        int NextNumber = 0;
        if( MinIndex<5 )
        {
            // 第一個號碼
            for (int j = 0; j < 5; j++)
                if (m_Board [MinIndex, j] != 0) {
                    NextNumber = m_Board [MinIndex, j];
                    break;
                }
        }
        // 決定出牌,行方向
        else if(MinIndex <10)
        {
            // 第一個號碼
            for (int i = 0; i < 5; i++)
                if (m_Board [i, MinIndex - 5] != 0) {
                    NextNumber = m_Board [i, MinIndex - 5];
                    break;
                }
        }
        // TODO,左斜(左上->右下)
        else if(MinIndex == 10)
        {
            for(int i = 0; i < 5; i++)
            {
                if(m_Board[i,i]!= 0)
                {
                    NextNumber = m_Board[i, i];
                    Debug.Log("電腦選擇左斜");
                    break;
                }
            }
        }
        // TODO,右斜(右上->左下)
        else
        {
            for(int i = 4;i>=0;i--)
                if (m_Board[i, 4 - i] != 0)
                {
                    NextNumber = m_Board[i, 4 - i];
                    Debug.Log("電腦選擇右斜");
                    break;
                }
        }
        */
				Debug.Log("電腦出牌[" + NextNumber + "]" + "價值 " + MaxPrice);
				return NextNumber;
			}
		}