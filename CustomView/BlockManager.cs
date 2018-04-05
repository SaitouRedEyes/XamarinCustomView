
using System.Collections.Generic;

namespace CustomView
{
    class BlockManager
    {
        private static BlockManager instance;

        private int lines, columns;
        public List<Block> blocks;

        private BlockManager()
        {
            lines = 4;
            columns = 6;

            blocks = new List<Block>();

            SetupBlocks();
        }

        public static BlockManager getInstance()
        {
            if (instance == null)
                instance = new BlockManager();

            return instance;
        }

        public void SetupBlocks()
        {
            blocks.Clear();

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Block block = new Block(i, j, columns);
                    blocks.Add(block);
                }
            }
        }
    }
}