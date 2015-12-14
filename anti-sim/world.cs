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

        private ArrayList regions = new ArrayList();

        public World()
        {
            Console.WriteLine("Created world");
            this.setup(0, 0);
        }

        public void setup(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.regions.Clear();

            for (int x = 0; x < width; x ++)
            {
                for (int y = 0; y < height; y ++)
                {
                    this.regions.Add(new Region());
                }
            }

            Console.WriteLine("Set up world with dimensions {0}x{1}", this.width, this.height);
        }

        public Region getRegion(int x, int y)
        {
            return (Region)this.regions[y * this.width + x];
        }
    }
}
