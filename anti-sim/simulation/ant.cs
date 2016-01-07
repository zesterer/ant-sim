using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
        class Ant
        {
            private Common.Vec2 position;
            private Common.Vec2 velocity;
            private int time = 0;

            public Ant()
            {
                Console.WriteLine("Created ant");
            }

			public Ant(Common.Vec2 position)
			{
				this.position = position;
				Console.WriteLine("Created ant at ({0}, {1})", position.X, position.Y);
			}

            public Common.Vec2 Position
            {
                get { return this.position; }
                set { this.position = value; }
            }

            void Move(Common.Vec2 vec)
            {
                this.velocity += vec;
            }

			public void placeRandomly(World world, Random generator = null)
			{
				if (generator == null)
					generator = new Random(142857);
				
				this.position = new Common.Vec2(generator.Next(0, world.Size.x), generator.Next(0, world.Size.y));
			}

			public void Tick(Random generator = null)
            {
				if (generator == null)
					generator = new Random(142857);
				
                //Do stuff
				this.Move(new Common.Vec2(generator.Next(-1, 2), generator.Next(-1, 2)));
            }

			public void PostTick(World world)
            {
                this.Position += this.velocity;
				this.velocity = new Common.Vec2(0, 0);

				this.Position = (this.Position + world.Size) % world.Size;

                this.time++;
            }
        }
    }
}