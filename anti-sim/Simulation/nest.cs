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
		class Nest
		{
			private Common.Vec2 position;
			private int foodCargo = 0;

			public Nest ()
			{
			}

			public int FoodCargo
			{
				get { return this.foodCargo; }
				set { this.foodCargo = value; }
			}

			public Common.Vec2 Position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public void PlaceRandomly(World world, Random generator = null)
			{
				if (generator == null)
					generator = new Random(142857);

				this.position = new Common.Vec2(generator.Next(0, world.Size.x), generator.Next(0, world.Size.y));
			}
		}
	}
}

