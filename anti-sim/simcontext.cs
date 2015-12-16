using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace anti_sim
{
    class SimContext
    {
        private World world;
		private List<Ant> ants = new List<Ant>();

        public SimContext()
        {
            world = new World();
            world.Setup(64, 64);

            //Create a few test ants
            for (int i = 0; i < 10; i++)
                ants.Add(new Ant());
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
				ant.Tick();

			//Tick the world
			this.world.Tick();
		}
    }
}
