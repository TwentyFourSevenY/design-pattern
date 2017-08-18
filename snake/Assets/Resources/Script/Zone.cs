using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone 
{
	private const int ROW_MIN = 0;
	private const int COL_MIN = 0;
	private static int ROW_MAX{ get; set; }
	private static int COL_MAX{ get; set; }

	private static Zone zone;
	private static Object obj = new Object();
	public static Zone Instance {
		get {
			if (zone == null) {
				lock (obj) {
					if (zone == null) {
						zone = new Zone (Game.WEIGHT, Game.HEIGHT);
					}
				}
			}
			return zone;
		}
	}
	private Zone(int row_max, int col_max)
	{
		ROW_MAX = row_max;
		COL_MAX = col_max;
	}
		
	public bool IsHitZone(Node node)
	{
		return HitZoneAsThrough (node);
		//return HitZoneAsDead (node);
	}

	private bool HitZoneAsDead(Node node)
	{
		if (node.row < ROW_MIN || node.row >= ROW_MAX)
			return true;
		if (node.col < COL_MIN || node.col >= COL_MAX)
			return true;
		return false;
	}

	private bool HitZoneAsThrough(Node node)
	{
		node.row = node.row < ROW_MIN ? ROW_MAX - 1 : node.row;
		node.col = node.col < COL_MIN ? COL_MAX - 1 : node.col;
		node.row = node.row >= ROW_MAX ? ROW_MIN : node.row;
		node.col = node.col >= COL_MAX ? COL_MIN : node.col;
		return false;
	}
}