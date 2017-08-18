using System;

namespace tetris {
	//enum最大好處有是限制的功用，用enum當作參數可以限制呼叫者只能傳enum裡面的值
	//用以旋轉後朝向哪邊
	enum EM_ROTATE_DIRECTION {UP=1, RIGHT=2, DOWN=3, LEFT=4}
	//用以使用者按下哪一種鍵盤
	enum EM_MOVE_METHOD {ROTATE=0, UP=1, RIGHT=2, DOWN=3, LEFT=4}

	class Tetris
	{
		public int[,] ContainerBody { 
			get { return m_block_container.MainContainer; }
		}
		public int[,] ContainerNext {
			get { return m_block_next.block; }
		}
		public bool IsGameOver {
			get {
				return m_is_game_over;
			}
			set { 
				m_is_game_over = value;
			}
		}

		public Tetris(int height, int width)
		{
			HEIGHT = height;
			WIDTH = width;
			//產生相關件
			Container.Init(HEIGHT,WIDTH);
			m_block_container = Container.Instance ();
			m_block_next = m_random_factory.CreateBlock ();
			MigrateNextBlockToContainer ();
		}

		public void MoveBlock(EM_MOVE_METHOD moveMethod)
		{
			switch (moveMethod) {
			case EM_MOVE_METHOD.DOWN://down
				m_block_container.MoveBlock (EM_MOVE_METHOD.DOWN);
				break;
			case EM_MOVE_METHOD.RIGHT://right
				m_block_container.MoveBlock (EM_MOVE_METHOD.RIGHT);
				break;
			case EM_MOVE_METHOD.LEFT://left
				m_block_container.MoveBlock (EM_MOVE_METHOD.LEFT);
				break;
			case EM_MOVE_METHOD.ROTATE: 
				m_block_container.RotateBlock ();
				break;
			default:
				break;
			}

			if (m_block_container.IsLowest)
				MigrateNextBlockToContainer();

			m_block_container.CheckRemoveRow ();
		}


		bool m_is_game_over = false;
		IContainer m_block_container;
		IBlock m_block_next;
		IBlockFactory m_random_factory = new BlockRandomFactory();

		readonly int HEIGHT;
		readonly int WIDTH;

		void MigrateNextBlockToContainer()
		{
			if (!m_block_container.AddBlock (m_block_next))
				IsGameOver = true;
			m_block_next = m_random_factory.CreateBlock ();
		}
	}
}