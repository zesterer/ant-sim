using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
		enum AntState
		{
			RANDOM_WALK = 0,
			COLLECT_FOOD = 1,
			RETURN_FOOD = 2,
		}

		struct AntKnowledge
		{
			public Common.Vec2 FoodPosition;
			public Common.Vec2 NestPosition;
		}

        class Ant
        {
            private Common.Vec2 position;
            private Common.Vec2 velocity;
            
			private int time = 0;

			private AntState state = AntState.RANDOM_WALK;
			private AntKnowledge knowledge;
			private int foodCargo = 0;

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

			public void PlaceRandomly(World world, Random generator = null)
			{
				if (generator == null)
					generator = new Random(142857);
				
				this.position = new Common.Vec2(generator.Next(0, world.Size.x), generator.Next(0, world.Size.y));
			}

			public void StepTowards(Common.Vec2 target)
			{
				Common.Vec2 relative = target - this.Position;
				int dimensionalMax = Math.Max(Math.Abs(relative.X), Math.Abs(relative.Y));

				if (dimensionalMax > 0)
					this.Move(new Common.Vec2(relative.X, relative.Y) / dimensionalMax);
			}

			public void Tick(Random generator = null)
            {
				if (generator == null)
					generator = new Random(142857);
				
                //Do stuff
				switch (this.state)
				{
					case AntState.RANDOM_WALK:
						this.Move(new Common.Vec2(generator.Next(-1, 2), generator.Next(-1, 2)));
						break;

					case AntState.COLLECT_FOOD:
						this.StepTowards(this.knowledge.FoodPosition);
						break;

					case AntState.RETURN_FOOD:
						this.StepTowards(this.knowledge.NestPosition);
						break;
				}
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