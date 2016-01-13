using System;

namespace AntSim
{
	namespace Simulation
	{
		class Entity
		{
			protected Common.Vec2 position;
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
		}
	}
}

