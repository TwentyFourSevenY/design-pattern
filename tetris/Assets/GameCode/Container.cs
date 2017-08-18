using System;

namespace tetris {
	interface IContainer{
		int[,] MainContainer { get;}
		bool IsLowest{ get;}
		bool AddBlock(IBlock newBlock);
		void MoveBlock(EM_MOVE_METHOD moveMethod);
		void RotateBlock();
		void CheckRemoveRow();
	}
	class Container : IContainer
	{
		//初始化
		public static void Init(int height, int witdh){
			HEIGHT = height;
			WIDTH = witdh;
			DestroyInstance();
		}
		//單例模式
		public static Container Instance(){
			return m_container;
		}

		//開啟新局時，用以刪除單例模式的實體
		public static void DestroyInstance(){
			m_container = new Container (HEIGHT, WIDTH);
		}
		//供繪圖取得主容器
		public int[,] MainContainer {
			get {
				return m_main_container;
			}
		}
		//是否已經降到最底部
		public bool IsLowest {
			get {
				return m_is_in_place;
			}
			private set { 
				m_is_in_place = value;
			}
		}

		//加入失敗傳回false表示結束
		public bool AddBlock(IBlock newblock)
		{
			CopyMainToSpare ();

			m_current_block_position_x = WIDTH/2;
			m_current_block_position_y = 0;
			m_current_block = newblock;

			IsLowest = false;

			if (IsAbnormal ()) {
				return false;
			}
			Confirm ();
			return true;
		}

		public void MoveBlock(EM_MOVE_METHOD moveMethod)
		{
			Move (moveMethod);
			if (IsAbnormal ()) {
				MoveRollback (moveMethod);
				if (IsMoveToLowest (moveMethod)) {
					IsLowest = true;
				}
			}
			Confirm ();
		}

		public void RotateBlock()
		{
			Rotate ();
			if (IsAbnormal ()) {
				RotateRollback ();
			}
			Confirm ();
		}

		public void CheckRemoveRow()
		{
			int highest = 0;
			int lowest = HEIGHT - 1;
			int leftest = 0;
			int rightest = WIDTH;

			for (int y = lowest; y >= highest; y--) {	//CheckRowOfAll
				bool coverdOfEntireRow = true;
				for (int x = leftest; x < rightest; x++)	//CheckColOfRow
					if (m_spare_container [y, x] == 0)
						coverdOfEntireRow = false;
				if (coverdOfEntireRow) {
					RemoveRowAndAutoFall (y);
					y++;	//after remove current row is its upper row, so not checked
				}
			}
		}

		static Container m_container = new Container (HEIGHT, WIDTH);
		static int[,] m_main_container;
		static int[,] m_spare_container;
		static int m_current_block_position_x;
		static int m_current_block_position_y;
		static bool m_is_in_place;
		static IBlock m_current_block;

		static int HEIGHT;
		static int WIDTH;

		Container(int height, int width)
		{
			HEIGHT = height;
			WIDTH = width;

			m_spare_container = new int[HEIGHT, WIDTH];
			m_main_container = new int[HEIGHT, WIDTH];

			Reset ();

			m_current_block_position_x = 0;
			m_current_block_position_y = 0;
		}

		static bool IsMoveToLowest (EM_MOVE_METHOD moveMethod)
		{
			return moveMethod == EM_MOVE_METHOD.DOWN;
		}

		void Confirm ()
		{
			CopySpareToMain ();
			PutBlockToMainContainer ();
		}

		void Reset ()
		{
			for (int y = 0; y < HEIGHT; y++)
				for (int x = 0; x < WIDTH; x++)
					m_main_container [y, x] = m_spare_container [y, x] = 0;
		}

		bool IsNormal()
		{
			for (int y = 0; y < 4; y++)
				for (int x = 0; x < 4; x++)
					if (IsNotNullOfCurrentBlock (y, x)) {
						if (IsOutsideBoundOfCurrentBlock (y, x) || IsNotNullOfSpareContainer (y, x))
							return false;
					}
			return true;
		}

		bool IsAbnormal()
		{
			return !IsNormal ();
		}

		void Move(EM_MOVE_METHOD moveMethod)
		{
			int forward = 1;
			SetPositionAccordingMove (moveMethod, forward);
		}

		void MoveRollback(EM_MOVE_METHOD moveMethod)
		{
			int forward = -1;
			SetPositionAccordingMove (moveMethod, forward);
		}

		void SetPositionAccordingMove (EM_MOVE_METHOD moveMethod, int forward)
		{
			switch (moveMethod) {
			case EM_MOVE_METHOD.DOWN:
				m_current_block_position_y += forward;
				break;
			case EM_MOVE_METHOD.RIGHT:
				m_current_block_position_x += forward;
				break;
			case EM_MOVE_METHOD.LEFT:
				m_current_block_position_x -= forward;
				break;
			default:
				break;
			}
		}

		void Rotate (){
			m_current_block.Rotate ();
		}

		void RotateRollback ()
		{
			m_current_block.RotateRollback ();
		}

		bool IsOutsideBoundOfCurrentBlock(int y, int x)
		{
			y += m_current_block_position_y;
			x += m_current_block_position_x;
			return (y >= HEIGHT) || x < 0 || x >= WIDTH;
		}

		bool IsNotNullOfCurrentBlock(int y, int x)
		{
			return IsNotNull (m_current_block.block, y, x);
		}

		bool IsNotNullOfSpareContainer(int y, int x)
		{
			y += m_current_block_position_y;
			x += m_current_block_position_x;
			return IsNotNull (m_spare_container, y, x);
		}

		bool IsNotNull(int[,] area, int y, int x)
		{
			return !(area [y, x] == 0);
		}

		void RemoveRowAndAutoFall(int y)
		{
			for (int i = y; i >= 0; i--)
				for (int m = 0; m < WIDTH; m++)
					if (i != 0)	//is not highest
						m_spare_container [i, m] = m_spare_container [i - 1, m];
					else
						m_spare_container [i, m] = 0;
			CopySpareToMain ();
		}

		void CopySpareToMain()
		{
			CopyA2B (m_spare_container, m_main_container);
		}

		void CopyMainToSpare()
		{
			CopyA2B (m_main_container, m_spare_container);
		}

	    void CopyA2B(int[,] a, int[,] b)
		{
			for (int y = 0; y < HEIGHT; y++)
				for (int x = 0; x < WIDTH; x++)
					b [y, x] = a [y, x];
		}

	    void PutBlockToMainContainer()
		{
			for (int y = 0; y < 4; y++)
				for (int x = 0; x < 4; x++)
					if ((x + m_current_block_position_x) >= 0 && (x + m_current_block_position_x) < WIDTH && (y + m_current_block_position_y) < HEIGHT)
						m_main_container [y + m_current_block_position_y, x + m_current_block_position_x] += m_current_block.block [y, x];
		}
	}
}