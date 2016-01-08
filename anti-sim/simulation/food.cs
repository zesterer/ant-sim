using System;

namespace AntSim
{
	namespace Simulation
	{
		class Food
		{
			private Common.Vec2 position;

			public Food()
			{
			}

			public Common.Vec2 Position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public void PlaceRandomly(World world)
			{
				Random generator = new Random();

				this.position = new Common.Vec2(generator.Next(0, world.Size.x), generator.Next(0, world.Size.y));
			}
		}
	}
}

