using System.Collections;
using System.Collections.Generic;

public class Node
{
	public int row;
	public int col;
	public Node(int r = -1, int c = -1)
	{
		row = r;
		col = c;
	}

	public void Copy(Node src)
	{
		row = src.row;
		col = src.col;
	}

	public static bool IsSameNode(Node a, Node b)
	{
		if (a.row == b.row && a.col == b.col)
			return true;
		return false;
	}
}
