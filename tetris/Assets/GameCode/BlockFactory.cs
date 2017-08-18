using System;

namespace tetris {
	interface IBlockFactory
	{
		IBlock CreateBlock();
	}
		
	class BlockOFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockO ();
		}
	}

	class BlockIFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockI ();
		}
	}

	class BlockLFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockL ();
		}
	}

	class BlockLOFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockLO ();
		}
	}
		
	class BlockSFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockS ();
		}
	}

	class BlockSOFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockSO ();
		}
	}

	class BlockEFactory : IBlockFactory
	{
		public IBlock CreateBlock()
		{
			return new BlockE ();
		}
	}

	class BlockRandomFactory : IBlockFactory
	{
		//隨機工廠是隨機一個值，然後呼叫其他工廠
		static Random random = new System.Random();
		public IBlock CreateBlock()
		{
			IBlockFactory block_factory = null;
			switch ((int)random.Next(1, 8)) {
			case 1:
				block_factory = new BlockOFactory ();
				break;
			case 2:
				block_factory = new BlockIFactory ();
				break;
			case 3:
				block_factory = new BlockLFactory ();
				break;
			case 4:
				block_factory = new BlockLOFactory ();
				break;
			case 5:
				block_factory = new BlockSFactory ();
				break;
			case 6:
				block_factory = new BlockSOFactory ();
				break;
			case 7:
				block_factory = new BlockEFactory ();
				break;
			default:
				break;
			}

			return block_factory.CreateBlock ();
		}
	}
}