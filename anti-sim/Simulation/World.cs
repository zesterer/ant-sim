using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AntSim
{
    namespace Simulation
    {
		//This class would have made searching for other ants much more efficient, but I ran out of
		//time to complete it. I have left the code here so that you can see that it was in an almost
		//completed state.
        class World
        {
            private int time;

            private int widthInRegions;
            private int heightInRegions;
            private int regionSize = 16;

            private List<Region> regions = new List<Region>();

            public World()
            {
                Console.WriteLine("Created world");
                this.Setup(0, 0);
            }

            public void Clear()
            {
                foreach (Region region in this.regions)
                    region.Clear();
            }

            public void Setup(int widthInRegions, int heightInRegions, int regionSize = 16)
            {
                this.widthInRegions = widthInRegions;
                this.heightInRegions = heightInRegions;
                this.regionSize = regionSize;

                this.regions.Clear();

                for (int x = 0; x < this.widthInRegions; x++)
                {
                    for (int y = 0; y < this.heightInRegions; y++)
                    {
                        this.regions.Add(new Region());
                    }
                }

                Console.WriteLine("Set up world with regional dimensions {0}x{1}", this.widthInRegions, this.heightInRegions);
            }

			public Common.Vec2 Size
			{
				get { return new Common.Vec2(this.widthInRegions * this.regionSize, this.heightInRegions * this.regionSize); }
			}

            public void Reset()
            {
                foreach (Region region in this.regions)
                    region.Clear();
            }

            public void Tick()
            {
                this.time++;
            }

            public Region GetRegion(int x, int y)
            {
                return (Region)this.regions[y * this.widthInRegions + x];
            }

            public Region GetRegionAt(int x, int y)
            {
                return (Region)this.GetRegion(x / this.regionSize, y / this.regionSize);
            }

            public void AddAnt(Ant ant)
            {
                this.GetRegionAt(ant.Position.X, ant.Position.Y).AppendAnt(ant);
            }
        }
    }
}
