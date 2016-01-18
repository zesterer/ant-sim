using System;

namespace AntSim
{
	namespace Simulation
	{
		class Entity
		{
			protected Common.Vec2 position; //Obvious
			protected Common.Vec2 velocity; //For per-tick movement, and to prevent ants jumping more than 1 pixel in each dimension per frame
			protected Context parent; //The parent object (i.e: a reference to the thing that created it)

			public Entity(Context parent) //The entity constructor
			{
				this.parent = parent; //Obvious too
			}

			public Common.Vec2 Position //Public getter and setter for the position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public void PlaceRandomly() //A method that scatters the entity across the world
			{
				this.position = new Common.Vec2(this.parent.Generator.Next(0, this.parent.World.Size.x), this.parent.Generator.Next(0, this.parent.World.Size.y));
			}

			public void Move(Common.Vec2 vec) //A method that moves the ant (by adding to the velocity)
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
				Common.Vec2 relative = target - this.Position; //Find the displacement between the current position and the target position
				int dimensionalMax = Math.Max(Math.Abs(relative.X), Math.Abs(relative.Y)); //The largest step needed in either dimension

				if (dimensionalMax > 0) //If the largest step is larger than 0 (i.e: we still need to move some distance)
					this.Move(new Common.Vec2(relative.X, relative.Y) / dimensionalMax); //Use integer revision to round the step down to 1 and 0 in either dimension
			}
		}
	}
}

