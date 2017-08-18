using System;

namespace tetris {
	interface IBlock
	{
		int[,] block{get;}
		void Rotate();
		void RotateRollback();
	}

	abstract class Block : IBlock
	{
		public Block()
		{
			SetRotateDirection (new Random ().Next (0, 5));
			Render ();
		}

		public int[,] block{
			get {
				return m_block;
			}
			protected set {
				m_block = value;
			}
		}

		public void Rotate()
		{
			SetRotateDirection (1);
			Render ();
		}

		//由於要恢復旋轉的時候，已經被旋轉過，因此再旋轉三次，即為原本的狀態
		public void RotateRollback()
		{
			SetRotateDirection (3);
			Render ();
		}

		protected EM_ROTATE_DIRECTION rotate_toward {
			get {
				return m_rotate_toward;
			}
			private set {
				m_rotate_toward = value;
			}
		}

		protected abstract void ConcreteRender();

		int[,] m_block = new int[4, 4];
		EM_ROTATE_DIRECTION m_rotate_toward = EM_ROTATE_DIRECTION.UP;


		void Render ()
		{
			ResetBlock ();
			ConcreteRender ();
		}

		void SetRotateDirection(int rotateTimes)
		{
			for (int i = 0; i < rotateTimes; i++) {
				rotate_toward++;
				if (rotate_toward == EM_ROTATE_DIRECTION.LEFT+1)
					rotate_toward = EM_ROTATE_DIRECTION.UP;
			}
		}

		void ResetBlock()
		{
			const int nil = 0;
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					block [i, j] = nil;
		}
	}

	class BlockO : Block
	{
		public BlockO() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_O = 1;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.UP:
			case EM_ROTATE_DIRECTION.RIGHT:
			case EM_ROTATE_DIRECTION.DOWN:
			case EM_ROTATE_DIRECTION.LEFT:
				block [2, 0] = shape_O;
				block [2, 1] = shape_O;
				block [3, 0] = shape_O;
				block [3, 1] = shape_O;
				break;
			default:
				break;
			}
		}
	}

	class BlockI : Block
	{
		public BlockI() : base() 
		{}
		protected override void ConcreteRender()
		{
			const int shape_I = 2;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.RIGHT:
			case EM_ROTATE_DIRECTION.LEFT:
				block [0, 1] = shape_I;
				block [1, 1] = shape_I;
				block [2, 1] = shape_I;
				block [3, 1] = shape_I;
				break;
			case EM_ROTATE_DIRECTION.UP:
			case EM_ROTATE_DIRECTION.DOWN:
				block [2, 0] = shape_I;
				block [2, 1] = shape_I;
				block [2, 2] = shape_I;
				block [2, 3] = shape_I;
				break;
			default:
				break;
			}
		}
	}

	class BlockL : Block
	{
		public BlockL() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_L = 3;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.UP:
				block [2, 0] = shape_L;
				block [2, 1] = shape_L;
				block [2, 2] = shape_L;
				block [3, 0] = shape_L;
				break;
			case EM_ROTATE_DIRECTION.RIGHT:
				block [1, 0] = shape_L;
				block [1, 1] = shape_L;
				block [2, 1] = shape_L;
				block [3, 1] = shape_L;
				break;
			case EM_ROTATE_DIRECTION.DOWN:
				block [1, 2] = shape_L;
				block [2, 0] = shape_L;
				block [2, 1] = shape_L;
				block [2, 2] = shape_L;
				break;
			case EM_ROTATE_DIRECTION.LEFT:
				block [1, 1] = shape_L;
				block [2, 1] = shape_L;
				block [3, 1] = shape_L;
				block [3, 2] = shape_L;
				break;
			default:
				break;
			}
		}
	}

	//LO  O: Opposite
	class BlockLO : Block
	{
		public BlockLO() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_LO = 4;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.UP:
				block [2, 0] = shape_LO;
				block [2, 1] = shape_LO;
				block [2, 2] = shape_LO;
				block [3, 2] = shape_LO;
				break;
			case EM_ROTATE_DIRECTION.RIGHT:
				block [3, 0] = shape_LO;
				block [1, 1] = shape_LO;
				block [2, 1] = shape_LO;
				block [3, 1] = shape_LO;
				break;
			case EM_ROTATE_DIRECTION.DOWN:
				block [1, 0] = shape_LO;
				block [2, 0] = shape_LO;
				block [2, 1] = shape_LO;
				block [2, 2] = shape_LO;
				break;
			case EM_ROTATE_DIRECTION.LEFT:
				block [1, 2] = shape_LO;
				block [2, 1] = shape_LO;
				block [3, 1] = shape_LO;
				block [1, 1] = shape_LO;
				break;
			default:
				break;
			}
		}
	}

	class BlockS : Block
	{
		public BlockS() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_S = 5;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.RIGHT:
			case EM_ROTATE_DIRECTION.LEFT:
				block [2, 0] = shape_S;
				block [2, 1] = shape_S;
				block [3, 1] = shape_S;
				block [3, 2] = shape_S;
				break;
			case EM_ROTATE_DIRECTION.UP:
			case EM_ROTATE_DIRECTION.DOWN:
				block [1, 2] = shape_S;
				block [2, 2] = shape_S;
				block [2, 1] = shape_S;
				block [3, 1] = shape_S;
				break;
			default:
				break;
			}
		}
	}

	//SO  O: Opposite
	class BlockSO : Block
	{
		public BlockSO() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_SO = 6;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.RIGHT:
			case EM_ROTATE_DIRECTION.LEFT:
				block [2, 1] = shape_SO;
				block [2, 2] = shape_SO;
				block [3, 0] = shape_SO;
				block [3, 1] = shape_SO;
				break;
			case EM_ROTATE_DIRECTION.UP:
			case EM_ROTATE_DIRECTION.DOWN:
				block [1, 0] = shape_SO;
				block [2, 0] = shape_SO;
				block [2, 1] = shape_SO;
				block [3, 1] = shape_SO;
				break;
			default:
				break;
			}
		}
	}

	class BlockE : Block
	{
		public BlockE() : base()
		{}
		protected override void ConcreteRender()
		{
			const int shape_E = 7;
			switch (rotate_toward) {
			case EM_ROTATE_DIRECTION.UP:
				block [2, 0] = shape_E;
				block [2, 1] = shape_E;
				block [2, 2] = shape_E;
				block [3, 1] = shape_E;
				break;
			case EM_ROTATE_DIRECTION.RIGHT:
				block [2, 0] = shape_E;
				block [1, 1] = shape_E;
				block [2, 1] = shape_E;
				block [3, 1] = shape_E;
				break;
			case EM_ROTATE_DIRECTION.DOWN:
				block [2, 0] = shape_E;
				block [2, 1] = shape_E;
				block [2, 2] = shape_E;
				block [1, 1] = shape_E;
				break;
			case EM_ROTATE_DIRECTION.LEFT:
				block [1, 1] = shape_E;
				block [2, 1] = shape_E;
				block [3, 1] = shape_E;
				block [2, 2] = shape_E;
				break;
			default:
				break;
			}
		}
	}
}