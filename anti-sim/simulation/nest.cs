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

			public Nest ()
			{
			}

			public Common.Vec2 Position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public void placeRandomly(World world)
			{
				Random generator = new Random();

				this.position = new Common.Vec2(generator.Next(0, world.Size.x), generator.Next(0, world.Size.y));
			}
		}
	}
}

