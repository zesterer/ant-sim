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
        class Context
        {
			private int time = 0;
            private World world;
            private List<Ant> ants = new List<Ant>();
			private List<Nest> nests = new List<Nest>();
			private List<Food> food = new List<Food>();

			Random generator = new Random(142857);

            public Context()
            {
				this.world = new World();
				this.world.Setup(16, 16, 16);

                //Create a few test ants
				for (int i = 0; i < 1000; i++)
				{
					Ant ant = new Ant();
					ant.PlaceRandomly(this.world, this.generator);
					this.ants.Add(ant);
				}

				//Create a few test nests
				for (int i = 0; i < 10; i++)
					this.nests.Add(new Nest());

				//Create a few pieces of test food
				for (int i = 0; i < 10; i++)
					this.food.Add(new Food());
            }

			public int Time
			{
				get { return this.time; }
			}

            public void Tick()
            {
                //Reset the world
                this.world.Reset();

                //Seed the ants within the world
                foreach (Ant ant in this.ants)
                    this.world.AddAnt(ant);

                //Tick the ants
                foreach (Ant ant in this.ants)
					ant.Tick(this.generator);

                //Finish the tick for the ants
                foreach (Ant ant in this.ants)
					ant.PostTick(this.world);

                //Tick the world
                this.world.Tick();
				this.time ++;
            }

			public int AntCount
			{
				get { return this.ants.Count; }
			}

			public Ant getAnt(int index)
			{
				return this.ants[index];
			}
        }
    }
}