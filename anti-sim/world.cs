using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace anti_sim
{
    class World
    {
        private int width;
        private int height;

        private ArrayList cells = new ArrayList();

        public World()
        {
            Console.WriteLine("Created world");
            this.setup(0, 0);
        }

        public void setup(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.cells.Clear();

            for (int x = 0; x < width; x ++)
            {
                for (int y = 0; y < height; y ++)
                {
                    this.cells.Add(new Cell());
                }
            }

            Console.WriteLine("Set up world with dimensions {0}x{1}", this.width, this.height);
        }

        public Cell getCell(int x, int y)
        {
            return (Cell)this.cells[y * this.width + x];
        }
    }
}
