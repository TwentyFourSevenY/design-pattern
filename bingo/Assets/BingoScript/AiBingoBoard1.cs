using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// 賓果Ai
public class AiBingoBoard : BingoBoard 
{
    public AiBingoBoard()
	{
	}

	const int bound = 5;
	int[,] pointValue = new int[bound, bound];
	int HightestValueNumber = -1;

	public int GetNextNumber()
	{
		Reset ();
		CalcValue ();
		UseHighestValue ();

		//Debug.Log ("Ai1出牌[" + HighReturnOnInvestmentAddress + "]");
		return HightestValueNumber;
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
		CalcAllValue ();
		CalcSpecialValue ();
	}
	void CalcAllValue()
	{
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (m_Board [c, r] == 0) {
					AddCol (c);
					AddRow (r);
					AddSlash (c, r);
					AddBackSlash (c, r);
				}
			}
		}
	}
	void CalcSpecialValue ()
	{
		CalcSlashValue ();
		CalcBackSlashValue ();
	}
	void CalcSlashValue()
	{
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (c == r) {
					pointValue [c, r] += 4;
				}
			}
		}
	}
	void CalcBackSlashValue()
	{
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if ((bound-c-1) == r) {
					pointValue [c, r] += 4;
				}
			}
		}
	}
	void AddCol (int col)
	{
		for (int r = 0; r < bound; r++) {
			pointValue [col, r]++;
		}
	}
	void AddRow(int row)
	{
		for (int c = 0; c < bound; c++) {
			pointValue [c, row]++;
		}
	}
	void AddSlash(int col, int row)
	{
		if (col == row) {
			for (int c = 0; c < bound; c++) {
				for (int r = 0; r < bound; r++) {
					if (c == r) {
						pointValue [c, r]+=2;
					}
				}
			}
		}
	}
	void AddBackSlash(int col, int row)
	{
		if ((bound - col - 1) == row) {
			for (int c = 0; c < bound; c++) {
				for (int r = 0; r < bound; r++) {
					if ((bound-c-1) == r) {
						pointValue [c, r]+=2;
					}
				}
			}
		}
	}

	void UseHighestValue()
	{
		int value = 0;
		for (int c = 0; c < bound; c++) {
			for (int r = 0; r < bound; r++) {
				if (m_Board [c, r] == 0)
					continue;
				if (pointValue [c, r] > value) {
					value = pointValue [c, r];
					HightestValueNumber = m_Board [c, r];
				}
			}
		}
	}

	/*
    protected int[] m_LinePoint = new int[12];

    // 決定出牌
    public int GetNextNumber()
	{
		int lineindex = 0;
		int point = 0;

		//計算列值
		for (int i = 0; i < 5; i++) {
			point = 0;
			for (int j = 0; j < 5; j++)
				if (m_Board [i, j] > 0)
					point++;
			m_LinePoint [lineindex++] = point;
		}

		//計算行值
		for (int j = 0; j < 5; j++) {
			point = 0;
			for (int i = 0; i < 5; i++)
				if (m_Board [i, j] > 0)
					point++;
			m_LinePoint [lineindex++] = point;
		}

		// TODO:左斜,右斜
		//m_LinePoint [lineindex++] = 5;
		//m_LinePoint [lineindex++] = 5;

		//計算左斜
		point = 0;
		for (int i=0; i<5; i++){
			if (m_Board [i, i] > 0)
				point++;
		}
		m_LinePoint [lineindex++] = point;
		//計算右斜
		point = 0;
		for (int i=0; i<5; i++){
			if (m_Board [i, 4-i] > 0)
				point++;
		}
		m_LinePoint [lineindex++] = point;

		// 取得最少分數者
		int MinNum = 6;
		int MinIndex = -1;
		for (lineindex = 0; lineindex < 12; ++lineindex)
			if (m_LinePoint [lineindex] != 0 && m_LinePoint [lineindex] < MinNum) {
				MinNum = m_LinePoint [lineindex];
				MinIndex = lineindex;
			}

		// 決定出牌,列方向
		int NextNumber = 0;

        // TODO,左斜(左上->右下)
        if (MinIndex == 10) {
			for (int i = 0; i < 5; i++)
				if (m_Board [i, i] != 0) {
					NextNumber = m_Board [i, i];
					break;
				}
		}
        // TODO,右斜(右上->左下)
		else if (MinIndex == 11) {
			for (int i = 0; i < 5; i++)
				if (m_Board [i, 4-i] != 0) {
					NextNumber = m_Board [i, 4-i];
					break;
				}
		}
		else if (MinIndex < 5) {
			// 第一個號碼
			for (int j = 0; j < 5; j++)
				if (m_Board [MinIndex, j] != 0) {
					NextNumber = m_Board [MinIndex, j];
					break;
				}
		}
		// 決定出牌,行方向
		else {
			// 第一個號碼
			for (int i = 0; i < 5; i++)
				if (m_Board [i, MinIndex - 5] != 0) {
					NextNumber = m_Board [i, MinIndex - 5];
					break;
				}
		}

		Debug.Log ("電腦出牌[" + NextNumber + "]");
		return NextNumber;
	}
	*/
}
