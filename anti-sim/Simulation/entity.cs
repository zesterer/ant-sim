using System;

namespace AntSim
{
	namespace Simulation
	{
		class Entity
		{
			protected Common.Vec2 position;
			protected Common.Vec2 velocity;
			protected Context parent;

			public Entity(Context parent)
			{
				this.parent = parent;
			}

			public Common.Vec2 Position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public void PlaceRandomly()
			{
				this.position = new Common.Vec2(this.parent.Generator.Next(0, this.parent.World.Size.x), this.parent.Generator.Next(0, this.parent.World.Size.y));
			}

			public void Move(Common.Vec2 vec)
			{
				this.velocity += vec;
			}

			//I was going to hack together an implementation of Bresenham's algorithm, but
			//if this project required proper movement planning, I'd have used a continuous
			//world with communication distance thresholds instead. Also, Bresenham needs
			//inputs such as the target and source position (which we don't have on a
			//locality level).
			public void StepTowards(Common.Vec2 target)
			{
				Common.Vec2 relative = target - this.Position;
				int dimensionalMax = Math.Max(Math.Abs(relative.X), Math.Abs(relative.Y));

				if (dimensionalMax > 0)
					this.Move(new Common.Vec2(relative.X, relative.Y) / dimensionalMax);
			}
		}
	}
}

